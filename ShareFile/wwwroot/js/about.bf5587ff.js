(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["about"],{"2db4":function(t,e,s){"use strict";var a=s("ade3"),i=(s("a9e3"),s("caad"),s("ca71"),s("8dd9")),n=s("a9ad"),o=s("7560"),r=s("f2e7"),c=s("fe6c"),l=s("58df"),u=s("80d2"),d=s("d9bd");e["a"]=Object(l["a"])(i["a"],n["a"],r["a"],Object(c["b"])(["absolute","bottom","left","right","top"])).extend({name:"v-snackbar",props:{app:Boolean,centered:Boolean,contentClass:{type:String,default:""},multiLine:Boolean,text:Boolean,timeout:{type:[Number,String],default:5e3},transition:{type:[Boolean,String],default:"v-snack-transition",validator:function(t){return"string"===typeof t||!1===t}},vertical:Boolean},data:function(){return{activeTimeout:-1}},computed:{classes:function(){return{"v-snack--absolute":this.absolute,"v-snack--active":this.isActive,"v-snack--bottom":this.bottom||!this.top,"v-snack--centered":this.centered,"v-snack--has-background":this.hasBackground,"v-snack--left":this.left,"v-snack--multi-line":this.multiLine&&!this.vertical,"v-snack--right":this.right,"v-snack--text":this.text,"v-snack--top":this.top,"v-snack--vertical":this.vertical}},hasBackground:function(){return!this.text&&!this.outlined},isDark:function(){return this.hasBackground?!this.light:o["a"].options.computed.isDark.call(this)},styles:function(){if(this.absolute)return{};var t=this.$vuetify.application,e=t.bar,s=t.bottom,a=t.footer,i=t.insetFooter,n=t.left,o=t.right,r=t.top;return{paddingBottom:Object(u["g"])(s+a+i),paddingLeft:this.app?Object(u["g"])(n):void 0,paddingRight:this.app?Object(u["g"])(o):void 0,paddingTop:Object(u["g"])(e+r)}}},watch:{isActive:"setTimeout",timeout:"setTimeout"},mounted:function(){this.isActive&&this.setTimeout()},created:function(){this.$attrs.hasOwnProperty("auto-height")&&Object(d["e"])("auto-height",this),0==this.timeout&&Object(d["d"])('timeout="0"',"-1",this)},methods:{genActions:function(){return this.$createElement("div",{staticClass:"v-snack__action "},[Object(u["s"])(this,"action",{attrs:{class:"v-snack__btn"}})])},genContent:function(){return this.$createElement("div",{staticClass:"v-snack__content",class:Object(a["a"])({},this.contentClass,!0),attrs:{role:"status","aria-live":"polite"}},[Object(u["s"])(this)])},genWrapper:function(){var t=this,e=this.hasBackground?this.setBackgroundColor:this.setTextColor,s=e(this.color,{staticClass:"v-snack__wrapper",class:i["a"].options.computed.classes.call(this),style:i["a"].options.computed.styles.call(this),directives:[{name:"show",value:this.isActive}],on:{pointerenter:function(){return window.clearTimeout(t.activeTimeout)},pointerleave:this.setTimeout}});return this.$createElement("div",s,[this.genContent(),this.genActions()])},genTransition:function(){return this.$createElement("transition",{props:{name:this.transition}},[this.genWrapper()])},setTimeout:function(){var t=this;window.clearTimeout(this.activeTimeout);var e=Number(this.timeout);this.isActive&&![0,-1].includes(e)&&(this.activeTimeout=window.setTimeout((function(){t.isActive=!1}),e))}},render:function(t){return t("div",{staticClass:"v-snack",class:this.classes,style:this.styles},[!1!==this.transition?this.genTransition():this.genWrapper()])}})},"615b":function(t,e,s){},"99d9":function(t,e,s){"use strict";s.d(e,"a",(function(){return n})),s.d(e,"b",(function(){return o})),s.d(e,"c",(function(){return r})),s.d(e,"d",(function(){return c}));var a=s("b0af"),i=s("80d2"),n=Object(i["i"])("v-card__actions"),o=Object(i["i"])("v-card__subtitle"),r=Object(i["i"])("v-card__text"),c=Object(i["i"])("v-card__title");a["a"]},b0af:function(t,e,s){"use strict";var a=s("5530"),i=(s("a9e3"),s("0481"),s("615b"),s("10d2")),n=s("297c"),o=s("1c87"),r=s("58df");e["a"]=Object(r["a"])(n["a"],o["a"],i["a"]).extend({name:"v-card",props:{flat:Boolean,hover:Boolean,img:String,link:Boolean,loaderHeight:{type:[Number,String],default:4},raised:Boolean},computed:{classes:function(){return Object(a["a"])(Object(a["a"])({"v-card":!0},o["a"].options.computed.classes.call(this)),{},{"v-card--flat":this.flat,"v-card--hover":this.hover,"v-card--link":this.isClickable,"v-card--loading":this.loading,"v-card--disabled":this.disabled,"v-card--raised":this.raised},i["a"].options.computed.classes.call(this))},styles:function(){var t=Object(a["a"])({},i["a"].options.computed.styles.call(this));return this.img&&(t.background='url("'.concat(this.img,'") center center / cover no-repeat')),t}},methods:{genProgress:function(){var t=n["a"].options.methods.genProgress.call(this);return t?this.$createElement("div",{staticClass:"v-card__progress",key:"progress"},[t]):null}},render:function(t){var e=this.generateRouteLink(),s=e.tag,a=e.data;return a.style=this.styles,this.isClickable&&(a.attrs=a.attrs||{},a.attrs.tabindex=0),t(s,this.setBackgroundColor(this.color,a),[this.genProgress(),this.$slots.default])}})},ca71:function(t,e,s){},dd7b:function(t,e,s){"use strict";s.r(e);var a=function(){var t=this,e=t.$createElement,s=t._self._c||e;return s("v-container",[s("v-card",[s("v-card-title",[t._v(" 获取远程访问权限 ")]),s("v-card-subtitle",[s("v-row",[s("v-col",[s("v-text-field",{attrs:{"hide-details":"",solo:"",label:"访问密码",clearable:"",type:"password"},model:{value:t.key.key,callback:function(e){t.$set(t.key,"key",e)},expression:"key.key"}})],1)],1),s("v-col"),s("v-row",{attrs:{justify:"center"}},[s("v-btn",{attrs:{color:"success"},on:{click:function(e){return t.sendLogin()}}},[t._v(" 登录 ")])],1),s("v-col")],1)],1),s("v-snackbar",{attrs:{timeout:"3000",top:""},scopedSlots:t._u([{key:"action",fn:function(e){var a=e.attrs;return[s("v-btn",t._b({attrs:{color:"pink",text:""},on:{click:function(e){t.snackbar=!1}}},"v-btn",a,!1),[t._v(" 关闭 ")])]}}]),model:{value:t.snackbar,callback:function(e){t.snackbar=e},expression:"snackbar"}},[t._v(" "+t._s(t.message)+" ")])],1)},i=[],n={data:function(){return{key:{key:"",fileId:""},snackbar:!1,message:""}},methods:{sendLogin:function(){var t=this;this.httpPost("/web/login",this.key,(function(e){200==e.status?(t.message="登录成功！",t.snackbar=!0,t.$store.state.webInfoStatus=!0,t.$store.state.hasAdmin=!0,sessionStorage.setItem("admin",!0),console.log("login success!"),t.$router.push("/")):(t.message="错误！",t.snackbar=!0)}))}}},o=n,r=s("2877"),c=s("6544"),l=s.n(c),u=s("8336"),d=s("b0af"),v=s("99d9"),h=s("62ad"),b=s("a523"),f=s("0fd9"),p=s("2db4"),g=s("8654"),m=Object(r["a"])(o,a,i,!1,null,null,null);e["default"]=m.exports;l()(m,{VBtn:u["a"],VCard:d["a"],VCardSubtitle:v["b"],VCardTitle:v["d"],VCol:h["a"],VContainer:b["a"],VRow:f["a"],VSnackbar:p["a"],VTextField:g["a"]})},f820:function(t,e,s){"use strict";s.r(e);var a=function(){var t=this,e=t.$createElement,s=t._self._c||e;return s("v-container",[s("v-row",[s("v-col",{attrs:{cols:"12"}},[s("h1",[t._v("关于")]),s("v-divider")],1)],1),s("v-row",[s("v-col",{attrs:{cols:"12"}},[t._v(" 本站是一个用来分享本地文件的文件分享网站。 ")])],1),s("v-row",[s("v-col",{attrs:{cols:"12"}},[s("h1",[t._v("使用帮助")]),s("v-divider")],1)],1),s("v-row",[s("v-col",{attrs:{cols:"12"}},[t._v(" 当你使用浏览器通过 http://127.0.0.1，http://localhost，http://[::1] 这三个地址访问本网站时，你将默认获取到最高权限，可以进行所有操作。 "),s("br"),t._v(" 如果通过其他地址访问本站，你需要进行身份验证后才能进行修改操作。 "),s("br"),t._v(" 分享文件列表默认只展示未加密的公开分享，只有获取相应权限后你才能查看加密分享。 "),s("br"),t._v(" 如果使用加密分享，请将分享链接和密码一同发给你的好友。 ")])],1),s("v-row",[s("v-col",{attrs:{cols:"12"}},[s("h1",[t._v("浏览器建议：")]),s("v-divider")],1)],1),s("v-row",[s("v-col",{attrs:{cols:"12"}},[t._v(" 推荐使用最新版的 Chrome， 新 Edge， Firefox 访问此网站，使用其它浏览器可能造成页面无法正常显示，功能异常等 Bug ")])],1),s("v-row",[s("v-col",{attrs:{cols:"12"}},[t._v(" 另外请在官方网站进行浏览器下载，不推荐任何第三方网站 "),s("br"),t._v(" Chrome： "),s("a",{attrs:{href:"https://www.google.cn/chrome/",target:"_blank"}},[t._v("https://www.google.cn/chrome/")]),t._v(" "),s("br"),t._v(" 新 Edge ： "),s("a",{attrs:{href:"https://www.microsoft.com/zh-cn/edge",target:"_blank"}},[t._v("https://www.microsoft.com/zh-cn/edge")]),t._v(" "),s("br"),t._v(" Firefox： "),s("a",{attrs:{href:"https://www.firefox.com.cn/",target:"_blank"}},[t._v("https://www.mozilla.org/")])])],1)],1)},i=[],n=s("2877"),o=s("6544"),r=s.n(o),c=s("62ad"),l=s("a523"),u=s("ce7e"),d=s("0fd9"),v={},h=Object(n["a"])(v,a,i,!1,null,null,null);e["default"]=h.exports;r()(h,{VCol:c["a"],VContainer:l["a"],VDivider:u["a"],VRow:d["a"]})}}]);