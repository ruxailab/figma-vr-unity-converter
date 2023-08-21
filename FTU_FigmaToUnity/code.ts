// This shows the HTML page in "ui.html".
figma.showUI(__html__, { themeColors: true })
figma.ui.resize(700, 450)

async function componentCreate () {
  const components = []

  for (const selection of figma.currentPage.selection) {
    if(typeof selection.absoluteBoundingBox?.width === 'number' && selection.type === 'COMPONENT') {
      const width = selection.absoluteBoundingBox?.width / 100;
      const height = selection.absoluteBoundingBox?.height / 100;
      const image = await selection.exportAsync({ format: 'PNG', constraint: { type: 'SCALE', value: 2 } })
      const keys = Object.keys(selection.componentPropertyDefinitions)
      const property = {
        rotationX: Number(componentProperty(keys, selection, 'RotationX')) || 0,
        rotationY: Number(componentProperty(keys, selection, 'RotationY')) || 0,
        positionX: Number(componentProperty(keys, selection, 'PositionX')) || 0,
        positionY: Number(componentProperty(keys, selection, 'PositionY')) || 0,
        positionZ: Number(componentProperty(keys, selection, 'PositionZ')) || 0,
        visible: componentProperty(keys, selection, 'Visible') == 'true' || true,
      }
      components.push({ width, height, image, property })
    }
  }
  figma.ui.postMessage({isComponent: isComponent(), components: components})
}
componentCreate()

function componentProperty (keys: string[], selection: ComponentNode, search: string) {
  const key = keys.find(key => key.includes(search))
  if(key) return selection.componentPropertyDefinitions[key]?.defaultValue
  return null
}

function isComponent () {
  if(figma.currentPage.selection.length < 1) return false
  for(const node of figma.currentPage.selection) {
    if(node.type !== 'COMPONENT') return false
  }
  return true
}


figma.ui.onmessage = msg => {
  
  if (msg.type === 'apply') {
    const components = figma.currentPage.selection
    components.forEach((node, i) => {
      if(node.type === 'COMPONENT') {
        addComponent(node, 'RotationX', msg.components[i].rotationX.toString())
        addComponent(node, 'RotationY', msg.components[i].rotationY.toString())
        addComponent(node, 'PositionZ', msg.components[i].positionZ.toString())
        addComponent(node, 'PositionX', msg.components[i].positionX.toString())
        addComponent(node, 'PositionY', msg.components[i].positionY.toString())
        addComponent(node, 'Visible', msg.components[i].visible.toString())
      }
    })
    figma.closePlugin()
  } 
  
  else if(msg.type == 'close') {
    figma.closePlugin()
  }


  function addComponent(node: ComponentNode, text: string, value: string) {
    let isValue = false;
    let nameProperty = ''
    const propertys = Object.keys(node.componentPropertyDefinitions)
    for(const property of propertys) {
      if(property.includes(text)){
        isValue = true
        nameProperty = property
      }
    }
    if(!isValue)
      node.addComponentProperty(text, 'TEXT', value)
    else
      node.editComponentProperty(nameProperty, {defaultValue: value})
  }
}