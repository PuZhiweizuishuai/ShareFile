(function(e){function t(t){for(var o,a,c=t[0],u=t[1],s=t[2],l=0,h=[];l<c.length;l++)a=c[l],Object.prototype.hasOwnProperty.call(r,a)&&r[a]&&h.push(r[a][0]),r[a]=0;for(o in u)Object.prototype.hasOwnProperty.call(u,o)&&(e[o]=u[o]);f&&f(t);while(h.length)h.shift()();return i.push.apply(i,s||[]),n()}function n(){for(var e,t=0;t<i.length;t++){for(var n=i[t],o=!0,a=1;a<n.length;a++){var c=n[a];0!==r[c]&&(o=!1)}o&&(i.splice(t--,1),e=u(u.s=n[0]))}return e}var o={},a={app:0},r={app:0},i=[];function c(e){return u.p+"js/"+({about:"about"}[e]||e)+"."+{"chunk-73a15848":"d6876f5b",about:"bf5587ff","chunk-1b2bf150":"49d946ba","chunk-378478d8":"29c2f4ee","chunk-70fdb911":"d8c625a6","chunk-f84aba12":"93817051","chunk-1b46a52e":"c6d96718","chunk-708c3ade":"b66e99d1"}[e]+".js"}function u(t){if(o[t])return o[t].exports;var n=o[t]={i:t,l:!1,exports:{}};return e[t].call(n.exports,n,n.exports,u),n.l=!0,n.exports}u.e=function(e){var t=[],n={"chunk-73a15848":1,about:1,"chunk-1b2bf150":1,"chunk-378478d8":1,"chunk-70fdb911":1,"chunk-f84aba12":1,"chunk-1b46a52e":1,"chunk-708c3ade":1};a[e]?t.push(a[e]):0!==a[e]&&n[e]&&t.push(a[e]=new Promise((function(t,n){for(var o="css/"+({about:"about"}[e]||e)+"."+{"chunk-73a15848":"be2890ce",about:"831c0648","chunk-1b2bf150":"831c0648","chunk-378478d8":"e8fc0b4e","chunk-70fdb911":"3673c396","chunk-f84aba12":"312104a3","chunk-1b46a52e":"0b94f07b","chunk-708c3ade":"0f6255fe"}[e]+".css",r=u.p+o,i=document.getElementsByTagName("link"),c=0;c<i.length;c++){var s=i[c],l=s.getAttribute("data-href")||s.getAttribute("href");if("stylesheet"===s.rel&&(l===o||l===r))return t()}var h=document.getElementsByTagName("style");for(c=0;c<h.length;c++){s=h[c],l=s.getAttribute("data-href");if(l===o||l===r)return t()}var f=document.createElement("link");f.rel="stylesheet",f.type="text/css",f.onload=t,f.onerror=function(t){var o=t&&t.target&&t.target.src||r,i=new Error("Loading CSS chunk "+e+" failed.\n("+o+")");i.code="CSS_CHUNK_LOAD_FAILED",i.request=o,delete a[e],f.parentNode.removeChild(f),n(i)},f.href=r;var d=document.getElementsByTagName("head")[0];d.appendChild(f)})).then((function(){a[e]=0})));var o=r[e];if(0!==o)if(o)t.push(o[2]);else{var i=new Promise((function(t,n){o=r[e]=[t,n]}));t.push(o[2]=i);var s,l=document.createElement("script");l.charset="utf-8",l.timeout=120,u.nc&&l.setAttribute("nonce",u.nc),l.src=c(e);var h=new Error;s=function(t){l.onerror=l.onload=null,clearTimeout(f);var n=r[e];if(0!==n){if(n){var o=t&&("load"===t.type?"missing":t.type),a=t&&t.target&&t.target.src;h.message="Loading chunk "+e+" failed.\n("+o+": "+a+")",h.name="ChunkLoadError",h.type=o,h.request=a,n[1](h)}r[e]=void 0}};var f=setTimeout((function(){s({type:"timeout",target:l})}),12e4);l.onerror=l.onload=s,document.head.appendChild(l)}return Promise.all(t)},u.m=e,u.c=o,u.d=function(e,t,n){u.o(e,t)||Object.defineProperty(e,t,{enumerable:!0,get:n})},u.r=function(e){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},u.t=function(e,t){if(1&t&&(e=u(e)),8&t)return e;if(4&t&&"object"===typeof e&&e&&e.__esModule)return e;var n=Object.create(null);if(u.r(n),Object.defineProperty(n,"default",{enumerable:!0,value:e}),2&t&&"string"!=typeof e)for(var o in e)u.d(n,o,function(t){return e[t]}.bind(null,o));return n},u.n=function(e){var t=e&&e.__esModule?function(){return e["default"]}:function(){return e};return u.d(t,"a",t),t},u.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)},u.p="/",u.oe=function(e){throw console.error(e),e};var s=window["webpackJsonp"]=window["webpackJsonp"]||[],l=s.push.bind(s);s.push=t,s=s.slice();for(var h=0;h<s.length;h++)t(s[h]);var f=l;i.push([0,"chunk-vendors"]),n()})({0:function(e,t,n){e.exports=n("56d7")},"1c1e":function(e,t,n){n("d3b7"),n("99af"),t.install=function(e){e.prototype.httpGet=function(e,t){fetch("".concat(this.SERVER_API_URL).concat(e),{headers:{"Content-Type":"application/json; charset=UTF-8"},method:"GET",credentials:"include"}).then((function(e){return e.json()})).then((function(e){t(e)})).catch((function(e){return console.log(e),null}))},e.prototype.httpPost=function(e,t,n){fetch("".concat(this.SERVER_API_URL).concat(e),{headers:{"Content-Type":"application/json; charset=UTF-8"},method:"POST",credentials:"include",body:JSON.stringify(t)}).then((function(e){return e.json()})).then((function(e){n(e)})).catch((function(e){return console.log(e),null}))},e.prototype.uploadFiles=function(e,t,n){fetch("".concat(this.SERVER_API_URL).concat(e),{headers:{"X-XSRF-TOKEN":this.$cookies.get("XSRF-TOKEN")},method:"POST",credentials:"include",body:t}).then((function(e){return e.json()})).then((function(e){n(e)})).catch((function(e){return console.log(e),null}))}}},"56d7":function(e,t,n){"use strict";n.r(t);n("e260"),n("e6cf"),n("cca6"),n("a79d");var o=n("2b0e"),a=function(){var e=this,t=e.$createElement,n=e._self._c||t;return n("router-view")},r=[],i={name:"App",data:function(){return{}}},c=i,u=n("2877"),s=Object(u["a"])(c,a,r,!1,null,null,null),l=s.exports,h=(n("d3b7"),n("3ca3"),n("ddb0"),n("8c4f")),f=function(){var e=this,t=e.$createElement,n=e._self._c||t;return n("v-app",{ref:"mainApp",attrs:{id:"inspire"}},[n("v-navigation-drawer",{attrs:{clipped:e.$vuetify.breakpoint.lgAndUp,app:""},model:{value:e.drawer,callback:function(t){e.drawer=t},expression:"drawer"}},[n("v-list",{attrs:{dense:""}},e._l(e.items,(function(t){return n("router-link",{key:t.text,attrs:{to:t.link}},[n("v-list-item",{attrs:{link:""}},[n("v-list-item-action",[n("v-icon",[e._v(e._s(t.icon))])],1),n("v-list-item-content",[n("v-list-item-title",[e._v(" "+e._s(t.text)+" ")])],1)],1)],1)})),1),n("v-divider"),n("v-footer",{staticStyle:{"background-color":"transparent"},attrs:{absolute:""}},[n("v-col",{staticClass:"text-center",attrs:{cols:"12"}},[n("a",{attrs:{href:"https://www.buguagaoshu.com",target:"_blank"}},[n("strong",[e._v("不挂高数")])]),e._v(" ©2021 - "+e._s((new Date).getFullYear())+" Created by "),n("a",{attrs:{href:"https://vuetifyjs.com/",target:"_blank"}},[n("strong",[e._v("Vuetify")])])])],1)],1),n("v-app-bar",{attrs:{"clipped-left":e.$vuetify.breakpoint.lgAndUp,app:"",color:"blue darken-3",dark:""}},[n("v-app-bar-nav-icon",{on:{click:function(t){e.drawer=!e.drawer}}}),n("v-toolbar-title",[e._v(e._s(this.$store.state.webInfo.name))])],1),n("v-main",[n("router-view")],1)],1)},d=[],p={data:function(){return{drawer:null,items:[],allItems:[{icon:"mdi-file",text:"文件浏览",link:"/",auth:!0},{icon:"mdi-share",text:"分享列表",link:"/filelist",auth:!1},{icon:"mdi-text",text:"二维码分享",link:"/qrcode",auth:!1},{icon:"mdi-wrench",text:"个性化",link:"/setting",auth:!0},{icon:"mdi-help",text:"关于",link:"/about",auth:!1}]}},created:function(){this.getPower()},mounted:function(){this.setBg()},methods:{getPower:function(){var e=this;this.httpGet("/reader/hasAdmin",(function(t){e.setItemList(t.data)}))},setItemList:function(e){if(e)this.items=this.allItems;else for(var t=0;t<this.allItems.length;t++)this.allItems[t].auth||this.items.push(this.allItems[t])},setBg:function(){if(""==this.$store.state.webInfo.background||null==this.$store.state.webInfo.background);else{console.log("set Bg");var e=document.querySelector("#inspire");e.style.backgroundImage="url(/api/file/bg)",e.style.backgroundSize="cover",e.style.backgroundRepeat="no-repeat"}""==this.$store.state.webInfo.logo||null==this.$store.state.webInfo.logo||(console.log("set logo"),document.querySelector('link[rel="icon"]').href="/api/file/bg?type=logo")}}},m=p,b=(n("cccb"),n("6544")),g=n.n(b),v=n("7496"),k=n("40dc"),y=n("5bc1"),w=n("62ad"),_=n("ce7e"),I=n("553a"),S=n("132d"),P=n("8860"),A=n("da13"),T=n("1800"),E=n("5d23"),j=n("f6c4"),V=n("f774"),x=n("2a7f"),L=Object(u["a"])(m,f,d,!1,null,null,null),O=L.exports;g()(L,{VApp:v["a"],VAppBar:k["a"],VAppBarNavIcon:y["a"],VCol:w["a"],VDivider:_["a"],VFooter:I["a"],VIcon:S["a"],VList:P["a"],VListItem:A["a"],VListItemAction:T["a"],VListItemContent:E["a"],VListItemTitle:E["b"],VMain:j["a"],VNavigationDrawer:V["a"],VToolbarTitle:x["a"]});var C=n("2f62");o["a"].use(C["a"]);var R=new C["a"].Store({state:{webInfoStatus:!0,webInfo:{},hasAdmin:!1},mutations:{},actions:{},modules:{}});o["a"].use(h["a"]);var F=[{path:"/",name:"Home",component:O,children:[{path:"/",name:"Home",component:function(){return Promise.all([n.e("chunk-73a15848"),n.e("chunk-f84aba12"),n.e("chunk-708c3ade")]).then(n.bind(null,"3f21"))},meta:{title:"文件分享服务 - 首页",requireAuth:!0}},{path:"/share/link/:id",name:"ShareLink",component:function(){return Promise.all([n.e("chunk-73a15848"),n.e("chunk-70fdb911")]).then(n.bind(null,"1a60"))},meta:{title:"文件分享服务"}},{path:"/filelist",name:"FileList",component:function(){return Promise.all([n.e("chunk-73a15848"),n.e("chunk-f84aba12"),n.e("chunk-1b46a52e")]).then(n.bind(null,"e125"))},meta:{title:"文件列表"}},{path:"/qrcode",name:"QrCode",component:function(){return Promise.all([n.e("chunk-73a15848"),n.e("chunk-378478d8")]).then(n.bind(null,"d692"))},meta:{title:"二维码分享"}},{path:"/setting",name:"Setting",component:function(){return Promise.all([n.e("chunk-73a15848"),n.e("chunk-1b2bf150")]).then(n.bind(null,"79d9"))},meta:{title:"个性化",requireAuth:!0}},{path:"/about",name:"About",component:function(){return Promise.all([n.e("chunk-73a15848"),n.e("about")]).then(n.bind(null,"f820"))}},{path:"/login",name:"Login",component:function(){return Promise.all([n.e("chunk-73a15848"),n.e("about")]).then(n.bind(null,"dd7b"))}}]}],U=new h["a"]({mode:"history",routes:F});U.beforeEach((function(e,t,n){return R.state.webInfoStatus&&(fetch("/api/web/info",{headers:{"Content-Type":"application/json; charset=UTF-8"},method:"GET"}).then((function(e){return e.json()})).then((function(e){R.state.webInfo=e.data,R.state.webInfoStatus=!1})),fetch("/api/reader/hasAdmin",{headers:{"Content-Type":"application/json; charset=UTF-8"},method:"GET"}).then((function(e){return e.json()})).then((function(e){R.state.hasAdmin=e.data,sessionStorage.setItem("admin",R.state.hasAdmin)}))),e.meta.title&&(document.title=e.meta.title),e.meta.requireAuth?"127.0.0.1"==location.hostname||"localhost"==location.hostname||"[::1]"==location.hostname||"true"==sessionStorage.getItem("admin")?n():n({path:"/login",query:{redirect:e.fullPath}}):n()}));var $=U,q=n("f309");o["a"].use(q["a"]);var N=new q["a"]({}),B=n("1c1e"),D=n.n(B);o["a"].config.productionTip=!1,o["a"].prototype.SERVER_API_URL="/api",o["a"].use(D.a),console.log("%c不挂高数出品 https://www.buguagaoshu.com","color: #1976d2;font-size:2em"),console.log("%cPowered by buguagaoshu 1.0.0 bete","color: #1976d2;font-size:1em"),new o["a"]({router:$,store:R,vuetify:N,render:function(e){return e(l)}}).$mount("#app")},"5ced":function(e,t,n){},cccb:function(e,t,n){"use strict";n("5ced")}});