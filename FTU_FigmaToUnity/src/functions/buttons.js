export const sceneBtn = () => {
  document.querySelector('#btn_scene').style.color = '#18a0fb'
  document.querySelector('#btn_settings').style.color = '#ffffff'
  document.querySelector('#scene').style.display = 'block'  
  document.querySelector('#settings').style.display = 'none'
}

export const setting = () => {
  document.querySelector('#btn_settings').style.color = '#18a0fb'
  document.querySelector('#btn_scene').style.color = '#ffffff'
  document.querySelector('#settings').style.display = 'block'
  document.querySelector('#scene').style.display = 'none'
}