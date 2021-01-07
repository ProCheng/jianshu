## babel的使用

> 1.  <span style='color:red'>`npm install --save-dev @babel/core @babel/cli @babel/preset-env @babel/node`</span>
>
> 2.  <span style='color:red'>`npm install --save @babel/polyfill`</span>
>
> 3.  项目跟目录创建文件<span style='color:red'>` babel.config.js`</span>
>
> 4.  <span style='color:red'>`babel.config.js`</span>文件内容如下代码
>
>    ``` js
>    const presets = [
>        ["@babel/env",{
>            targets:{
>                edge:"17",
>                firefox:"60",
>                chrome:"67",
>                safari:"11.1"
>            }
>        }]
>    ]
>    module.exports = { presets };
>    
>    ```
>
>5. 通过 <span style='color:red'>npx babel-node index.js </span>执行代码
>
>    

