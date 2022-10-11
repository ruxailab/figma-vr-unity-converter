## Figma - API

**IMPORTANTE**
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