// This shows the HTML page in "ui.html".
figma.showUI(__html__, { themeColors: true })
figma.ui.resize(700, 450)

let isComponent = () => {
  if(figma.currentPage.selection.length < 1){
    return false
  }
  for(const node of figma.currentPage.selection) {
    if(node.type !== 'COMPONENT'){
      return false
    }
  }
  return true
}

let componentCreate = async () => {
  let components = []
  for(let i=0; i<figma.currentPage.selection.length; i++){
    let select = figma.currentPage.selection[i]
    if(typeof(select.absoluteBoundingBox?.width) === 'number' && select.type === 'COMPONENT'){
      let width = select.absoluteBoundingBox?.width/100
      let height = select.absoluteBoundingBox?.height/100
      let image = await figma.currentPage.selection[i].exportAsync({
        format: 'PNG',
        constraint: { type: 'SCALE', value: 2 }
      })
      let propertys = select.componentPropertyDefinitions
      let property: { rotationX: number, rotationY: number, positionX: number, positionY: number, positionZ: number } = {rotationX: 0, rotationY: 0, positionX: 0, positionY: 0, positionZ: -1}
      if(Object.keys(propertys).length === 5) {
        const keys = Object.keys(propertys)
        keys.forEach( key => {
          if(key.includes('RotationX')) {
            property.rotationX = Number(propertys[key].defaultValue)
          }
          else if(key.includes('RotationY')) {
            property.rotationY = Number(propertys[key].defaultValue)
          }
          else if(key.includes('PositionX')) {
            property.positionX = Number(propertys[key].defaultValue)
          }
          else if(key.includes('PositionY')) {
            property.positionY = Number(propertys[key].defaultValue)
          }
          else if(key.includes('PositionZ')) {
            property.positionZ = Number(propertys[key].defaultValue)
          }
        })
      }
      let component: { width: number, height: number, image: object, property: object  } = { width, height, image, property }
      components.push(component)
    }
  }
  figma.ui.postMessage({isComponent: isComponent(), components: components})
}
componentCreate()

figma.ui.onmessage = msg => {
  
  if (msg.type === 'apply') {
    var components = figma.currentPage.selection
    components.forEach((node, i) => {
      if(node.type === 'COMPONENT'){
        addComponent(node, 'RotationX', msg.components[i].rotationX.toString())
        addComponent(node, 'RotationY', msg.components[i].rotationY.toString())
        addComponent(node, 'PositionZ', msg.components[i].positionZ.toString())
        addComponent(node, 'PositionX', msg.components[i].positionX.toString())
        addComponent(node, 'PositionY', msg.components[i].positionY.toString())
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