"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
// This shows the HTML page in "ui.html".
figma.showUI(__html__, { themeColors: true });
figma.ui.resize(700, 450);
let isComponent = () => {
    if (figma.currentPage.selection.length < 1) {
        return false;
    }
    for (const node of figma.currentPage.selection) {
        if (node.type !== 'COMPONENT') {
            return false;
        }
    }
    return true;
};
let componentCreate = () => __awaiter(void 0, void 0, void 0, function* () {
    var _a, _b, _c;
    let components = [];
    for (let i = 0; i < figma.currentPage.selection.length; i++) {
        let select = figma.currentPage.selection[i];
        if (typeof ((_a = select.absoluteBoundingBox) === null || _a === void 0 ? void 0 : _a.width) === 'number' && select.type === 'COMPONENT') {
            let width = ((_b = select.absoluteBoundingBox) === null || _b === void 0 ? void 0 : _b.width) / 100;
            let height = ((_c = select.absoluteBoundingBox) === null || _c === void 0 ? void 0 : _c.height) / 100;
            let image = yield figma.currentPage.selection[i].exportAsync({
                format: 'PNG',
                constraint: { type: 'SCALE', value: 2 }
            });
            let propertys = select.componentPropertyDefinitions;
            let property = {
                rotationX: 0,
                rotationY: 0,
                positionX: 0,
                positionY: 0,
                positionZ: -1,
                visiable: true,
            };
            const keys = Object.keys(propertys);
            keys.forEach(key => {
                if (key.includes('RotationX')) {
                    property.rotationX = Number(propertys[key].defaultValue);
                }
                else if (key.includes('RotationY')) {
                    property.rotationY = Number(propertys[key].defaultValue);
                }
                else if (key.includes('PositionX')) {
                    property.positionX = Number(propertys[key].defaultValue);
                }
                else if (key.includes('PositionY')) {
                    property.positionY = Number(propertys[key].defaultValue);
                }
                else if (key.includes('PositionZ')) {
                    property.positionZ = Number(propertys[key].defaultValue);
                }
                else if (key.includes('Visiable')) {
                    property.visiable = Boolean(propertys[key].defaultValue);
                }
            });
            let component = { width, height, image, property };
            components.push(component);
        }
    }
    figma.ui.postMessage({ isComponent: isComponent(), components: components });
});
componentCreate();
figma.ui.onmessage = msg => {
    if (msg.type === 'apply') {
        var components = figma.currentPage.selection;
        components.forEach((node, i) => {
            if (node.type === 'COMPONENT') {
                addComponent(node, 'RotationX', msg.components[i].rotationX.toString());
                addComponent(node, 'RotationY', msg.components[i].rotationY.toString());
                addComponent(node, 'PositionZ', msg.components[i].positionZ.toString());
                addComponent(node, 'PositionX', msg.components[i].positionX.toString());
                addComponent(node, 'PositionY', msg.components[i].positionY.toString());
                addComponent(node, 'Visiable', msg.components[i].visiable.toString());
            }
        });
        figma.closePlugin();
    }
    else if (msg.type == 'close') {
        figma.closePlugin();
    }
    function addComponent(node, text, value) {
        let isValue = false;
        let nameProperty = '';
        const propertys = Object.keys(node.componentPropertyDefinitions);
        for (const property of propertys) {
            if (property.includes(text)) {
                isValue = true;
                nameProperty = property;
            }
        }
        if (!isValue)
            node.addComponentProperty(text, 'TEXT', value);
        else
            node.editComponentProperty(nameProperty, { defaultValue: value });
    }
};
