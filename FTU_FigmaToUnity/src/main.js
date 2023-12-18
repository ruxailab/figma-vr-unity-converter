// Imports Three.js
import * as THREE from 'three'
import { OrbitControls } from 'three/addons/controls/OrbitControls.js'

// Imports Assets
import { human } from './assets/models/human'

// Imports Functions
import { sceneBtn, setting } from './functions/buttons'
import { loaderObj } from './functions/loaderObj'
import { mapEuler, mapRadianos } from './functions/maps'

onmessage = async event => {
  let data = event.data.pluginMessage
  console.log('oi')
  console.log(data)
  if(!data.isComponent) {
    alert('Selecione somente componentes')
    parent.postMessage({ pluginMessage: { type: 'close' } }, '*')
  }

  // Obtenha a largura e altura da tela
  const screenWidth = 520
  const screenHeight = 450

  // Renderer
  const renderer = new THREE.WebGLRenderer()
  renderer.setSize(screenWidth, screenHeight)
  renderer.outputColorSpace = THREE.SRGBColorSpace;
  document.querySelector('#threeJS').appendChild(renderer.domElement)

  // Scene
  const scene = new THREE.Scene()
  scene.background = new THREE.Color(0x121212)

  // Camera
  const camera = new THREE.PerspectiveCamera(75, screenWidth / screenHeight, 0.1, 1000)
  const controls = new OrbitControls(camera, renderer.domElement)
  camera.position.set(0, 0, 5)

  // Circle
  const radius = 10
  const sectors = 16
  const rings = 8
  const divisions = 64
  const circle = new THREE.PolarGridHelper(radius, sectors, rings, divisions)
  circle.position.y = -1
  scene.add(circle)

  const humanObj = await loaderObj(human)
  scene.add(humanObj)

  // Component
  const meshComponents = []

  for(const component of data.components) {
    const geometryComponent = new THREE.BoxGeometry(component.width, component.height, 0.001)
    const imageBlob = new Blob([component.image], {type: "image/png"})
    const url = URL.createObjectURL(imageBlob)
    const textureComponent = new THREE.TextureLoader().load(url, () =>{
      const materialComponent = new THREE.MeshBasicMaterial({ map: textureComponent })
      materialComponent.needsUpdate = true
      materialComponent.map.encoding = THREE.sRGBEncoding

      const meshComponent = new THREE.Mesh(geometryComponent, materialComponent)
      const { positionX, positionY, positionZ, rotationX, rotationY, visible } = component.property
      meshComponent.position.set(positionX, positionY, positionZ)
      meshComponent.rotation.set(mapEuler(rotationX), mapEuler(rotationY), 0)
      meshComponent.componentVisible = visible
      meshComponent.material.color.setRGB(visible ? 1 : 0.2, visible ? 1 : 0.2, visible ? 1 : 0.2)
      scene.add(meshComponent)
      meshComponents.push(meshComponent)
    })
  }

  // Click Object
  const pointer = new THREE.Vector2()
  const raycaster = new THREE.Raycaster()
  let select, line

  const onMouseClick = event => {
    pointer.x = (event.clientX / window.innerWidth) * 2 - 0.7
    pointer.y = -(event.clientY / window.innerHeight) * 2 + 1
    raycaster.setFromCamera(pointer, camera);
    const intersects = raycaster.intersectObjects(meshComponents);  

    if (intersects.length > 0) {
      if (select) scene.remove(line)
      select = intersects[0].object

      const { width, height } = select.geometry.parameters
      const geometry = new THREE.BoxGeometry(width, height, 0.001)
      const edges = new THREE.EdgesGeometry(geometry)
      line = new THREE.LineSegments(edges, new THREE.LineBasicMaterial({ color: 0x3e6deb }))

      const { position, rotation } = select;
      document.querySelector('#positionX').value = position.x
      document.querySelector('#positionY').value = position.y
      document.querySelector('#distance').value = -position.z
      document.querySelector('#rotationX').value = mapRadianos(rotation.x)
      document.querySelector('#rotationY').value = mapRadianos(rotation.y)
      document.querySelector('#visible').checked = select.componentVisible
      scene.add(line)
    }
    else {
      scene.remove(line)
      select = undefined
    }
  }

  const div = document.querySelector('#threeJS')
  div.addEventListener('dblclick', onMouseClick)

  // Animation
  function animate() {
    requestAnimationFrame(animate)
    controls.update()

    if(select){
      const { position, rotation } = select
      const { checked } = document.querySelector('#visible')

      position.x = document.querySelector('#positionX').value || 0
      position.y = document.querySelector('#positionY').value || 0
      position.z = -document.querySelector('#distance').value || 0
      rotation.x = mapEuler(document.querySelector('#rotationX').value) || 0
      rotation.y = mapEuler(document.querySelector('#rotationY').value) || 0

      select.componentVisible = checked
      select.material.color.setRGB(checked ? 1 : 0.2, checked ? 1 : 0.2, checked ? 1 : 0.2)

      line.position.copy(position)
      line.rotation.copy(rotation)
    }
    renderer.render(scene, camera)
  }
  animate()

  // Buttons
  const components = []
  document.querySelector('#apply').onclick = () => {
    for(const meshComponent of meshComponents){
      const component = {
      positionX: meshComponent.position.x,
      positionY: meshComponent.position.y,
      positionZ: meshComponent.position.z,
      rotationX: mapRadianos(meshComponent.rotation._x),
      rotationY: mapRadianos(meshComponent.rotation._y),
      visible: meshComponent.componentVisible,
      }
      components.push(component)
    }
    parent.postMessage({ pluginMessage: { type: 'apply', components: components } }, '*')
  }

  document.querySelector('#close').onclick = () => {
    parent.postMessage({ pluginMessage: { type: 'close' } }, '*')
  }

  document.querySelector('#btn_scene').addEventListener('click', sceneBtn)
  document.querySelector('#btn_settings').addEventListener('click', setting)

  document.querySelector('#showOrbit').addEventListener('click', () => {
    if(document.querySelector('#showOrbit').checked) return scene.add(circle)
    scene.remove(circle)
  })

  document.querySelector('#showUser').addEventListener('click', () => {
    if(document.querySelector('#showUser').checked) return scene.add(humanObj)
    scene.remove(humanObj)
  })

  document.querySelector('#colorOrbit').addEventListener('change', () => {
    circle.material.color = new THREE.Color(document.querySelector('#colorOrbit').value)
  })

  document.querySelector('#colorUser').addEventListener('change', () => {
    humanObj.children[0].material.emissive = new THREE.Color(document.querySelector('#colorUser').value)
  })
}