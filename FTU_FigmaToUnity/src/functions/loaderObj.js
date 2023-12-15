import * as THREE from 'three'
import { OBJLoader } from 'three/addons/loaders/OBJLoader.js'

import { mapEuler } from './maps'

export const loaderObj = async content => {
  const blob = new Blob([content], {type: 'text/plain'})
  const url = URL.createObjectURL(blob)

  try {
    const obj = await new Promise((resolve, reject) => {
      new OBJLoader().load(
        url,
        resolve,
        (xhr) => console.log((xhr.loaded / xhr.total * 100) + '% loaded'),
        reject
      )
    })

    obj.rotation.y = mapEuler(180)
    obj.scale.x = 0.2
    obj.scale.y = 0.2
    obj.scale.z = 0.2
    obj.position.y = -1
    obj.children[0].material.emissive = new THREE.Color('#ffffff')
    return obj
  } catch (error) {
    console.log(error)
  } finally {
    URL.revokeObjectURL(url)
  }
}