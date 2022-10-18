## Figma - API

**IMPORTANTE**
- Postman: https://orange-space-957236.postman.co/workspace/Liquid-Galaxy~d9f0f502-42b6-4da1-b34c-cacaf76b84bf/collection/21577195-86734ae6-cf68-4ac8-8aee-78992c835af9?action=share&creator=21577195

- Documentação Oficial: https://www.figma.com/developers/api

- Todas as requisições tem que passar na HEAD o token da seguinte forma:
~~~JSON
"X-Figma-Token": "token"
~~~

**Informações do Usuário**
~~~
GET https://api.figma.com/v1/me -H X-Figma-Token: token
~~~

**Obter Arquivo**
~~~
GET https://api.figma.com/v1/files/:key(URL) -H X-Figma-Token: token
~~~