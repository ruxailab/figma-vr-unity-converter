# FTU (Figma To Unity)

Below are the steps to get the plugin up and running.

This plugin model uses Typescript and NPM, two standard tools in JavaScript application creation.

First, download Node.js that comes with NPM. This will allow you to install TypeScript and other libraries. You can find the download link here:

  https://nodejs.org/en/download/

Then install all dependencies with the following command:

```bash
npm install
```
If you're familiar with JavaScript, TypeScript will look very familiar. In fact, valid JavaScript code
is already valid TypeScript code.

TypeScript adds type annotations to variables. This allows code editors such as Visual Studio Code
to provide information about the Figma API as you write code, as well as help catch bugs.

For more information, visit https://www.typescriptlang.org/

Using TypeScript requires a compiler to convert TypeScript (code.ts) to JavaScript (code.js) for the browser to run.

We recommend writing the TypeScript code using Visual Studio:

1. Download Visual Studio Code if you haven't already: https://code.visualstudio.com/.
2. Open that directory in Visual Studio Code.
3. Compile TypeScript to JavaScript: Press Ctrl-Shift -B in Windows or Command -Shift -B` for Mac. Then select watch-tsconfig.json.

The plugin has two essential files, code.ts and ui.html. The code.ts is responsible for all the data handling, while the ui.html where the entire user interface will be.