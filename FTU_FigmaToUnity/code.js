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
function componentCreate() {
    var _a, _b, _c;
    return __awaiter(this, void 0, void 0, function* () {
        const components = [];
        for (const selection of figma.currentPage.selection) {
            if (typeof ((_a = selection.absoluteBoundingBox) === null || _a === void 0 ? void 0 : _a.width) === 'number' && selection.type === 'COMPONENT') {
                const name = selection.name;
                const width = ((_b = selection.absoluteBoundingBox) === null || _b === void 0 ? void 0 : _b.width) / 100;
                const height = ((_c = selection.absoluteBoundingBox) === null || _c === void 0 ? void 0 : _c.height) / 100;
                const image = yield selection.exportAsync({ format: 'PNG', constraint: { type: 'SCALE', value: 3 } });
                const keys = Object.keys(selection.componentPropertyDefinitions);
                const property = {
                    rotationX: Number(componentProperty(keys, selection, 'RotationX')) || 0,
                    rotationY: Number(componentProperty(keys, selection, 'RotationY')) || 0,
                    rotationZ: Number(componentProperty(keys, selection, 'RotationZ')) || 0,
                    positionX: Number(componentProperty(keys, selection, 'PositionX')) || 0,
                    positionY: Number(componentProperty(keys, selection, 'PositionY')) || 0,
                    positionZ: Number(componentProperty(keys, selection, 'PositionZ')) || 0,
                    visible: componentProperty(keys, selection, 'Visible') == 'true' || true,
                };
                components.push({ name, width, height, image, property });
            }
        }
        figma.ui.postMessage({ isComponent: isComponent(), components: components });
    });
}
componentCreate();
function componentProperty(keys, selection, search) {
    var _a;
    const key = keys.find(key => key.includes(search));
    if (key)
        return (_a = selection.componentPropertyDefinitions[key]) === null || _a === void 0 ? void 0 : _a.defaultValue;
    return null;
}
function isComponent() {
    if (figma.currentPage.selection.length < 1)
        return false;
    for (const node of figma.currentPage.selection) {
        if (node.type !== 'COMPONENT')
            return false;
    }
    return true;
}
figma.ui.onmessage = msg => {
    if (msg.type === 'apply') {
        const components = figma.currentPage.selection;
        components.forEach((node, i) => {
            if (node.type === 'COMPONENT') {
                addComponent(node, 'RotationX', msg.components[i].rotationX.toString());
                addComponent(node, 'RotationY', msg.components[i].rotationY.toString());
                addComponent(node, 'RotationZ', msg.components[i].rotationZ.toString());
                addComponent(node, 'PositionZ', msg.components[i].positionZ.toString());
                addComponent(node, 'PositionX', msg.components[i].positionX.toString());
                addComponent(node, 'PositionY', msg.components[i].positionY.toString());
                addComponent(node, 'Visible', msg.components[i].visible.toString());
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
