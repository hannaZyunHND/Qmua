const modules = {}
const requireModule = require.context("./../", true, /store\.js$/);
requireModule.keys().forEach(function (key) {
    const modulePath = key.replace('./', '').replace('/store.js', ''); 
    const fileName = key.replace(modulePath+'/.', '');     

    modules[modulePath] = requireModule(fileName).default;
});
export default modules;
