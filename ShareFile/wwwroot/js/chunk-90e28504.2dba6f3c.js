(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-90e28504"],{"17b3":function(t,e,a){},e125:function(t,e,a){"use strict";a.r(e);var i=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("v-container",[a("v-row",[a("v-col",[a("h3",[t._v("所有分享")])])],1),a("v-row",[a("v-col",[a("v-data-table",{attrs:{headers:t.headers,items:t.fileList,"items-per-page":t.size,page:t.page,"hide-default-footer":""},on:{"update:page":function(e){t.page=e}},scopedSlots:t._u([{key:"item.name",fn:function(e){var i=e.item;return[a("v-icon",[t._v(" "+t._s(t.TYPE_ICON[i.type])+" ")]),t._v(t._s(i.name)+" ")]}},{key:"item.type",fn:function(e){var a=e.item;return[t._v(" "+t._s(t.TYPE[a.type])+" ")]}},{key:"item.expirationTime",fn:function(e){var i=e.item;return[0!=i.expirationTime?a("v-tooltip",{attrs:{top:""},scopedSlots:t._u([{key:"activator",fn:function(e){var n=e.on,s=e.attrs;return[a("v-chip",t._g(t._b({attrs:{color:"primary",dark:""}},"v-chip",s,!1),n),[t._v(" "+t._s(t.checkExpirationTime(i))+" ")])]}}],null,!0)},[a("span",[t._v(" 过期时间： "+t._s(t.TimeUtil.renderTime(i.expirationTime))+" ")])]):t._e(),0==i.expirationTime?a("span",[t._v(" "+t._s(t.checkExpirationTime(i))+" ")]):t._e()]}},{key:"item.hasKey",fn:function(e){var a=e.item;return[t._v(" "+t._s(a.hasKey?"是":"否")+" ")]}},{key:"item.actions",fn:function(e){var i=e.item;return[a("v-btn",{attrs:{depressed:"",color:"primary",small:""},on:{click:function(e){return t.seeFile(i)}}},[t._v(" 查看 ")]),t._v("    "),a("v-btn",{attrs:{depressed:"",color:"success",small:""},on:{click:function(e){return t.copyLink(i)}}},[t._v(" 复制链接 ")]),t.showDeletePower?a("span",[t._v("  ")]):t._e(),t.showDeletePower?a("v-btn",{attrs:{depressed:"",color:"error",small:""},on:{click:function(e){return t.deleteShare(i)}}},[t._v(" 删除 ")]):t._e()]}}])})],1)],1),a("v-col"),a("v-row",{attrs:{justify:"center"}},[a("v-pagination",{attrs:{length:t.totalPages,circle:"","total-visible":10},on:{input:t.pageChange},model:{value:t.page,callback:function(e){t.page=e},expression:"page"}})],1),a("v-col"),a("v-snackbar",{attrs:{timeout:"3000",top:""},scopedSlots:t._u([{key:"action",fn:function(e){var i=e.attrs;return[a("v-btn",t._b({attrs:{color:"pink",text:""},on:{click:function(e){t.snackbar=!1}}},"v-btn",i,!1),[t._v(" 关闭 ")])]}}]),model:{value:t.snackbar,callback:function(e){t.snackbar=e},expression:"snackbar"}},[t._v(" "+t._s(t.message)+" ")]),a("v-dialog",{attrs:{persistent:"","max-width":"290"},model:{value:t.deleteDialog,callback:function(e){t.deleteDialog=e},expression:"deleteDialog"}},[a("v-card",[a("v-card-title",{staticClass:"text-h5"},[t._v(" 你确定要删除这条分享吗？ ")]),a("v-card-text",[t._v(" "+t._s(t.nowDeleteItem.name)+" ")]),a("v-card-actions",[a("v-spacer"),a("v-btn",{attrs:{color:"green darken-1",text:""},on:{click:function(e){t.deleteDialog=!1}}},[t._v(" 关闭 ")]),a("v-btn",{attrs:{color:"green darken-1",text:""},on:{click:function(e){return t.sendDeleteItem()}}},[t._v(" 确定 ")])],1)],1)],1)],1)},n=[],s=(a("99af"),a("9878")),r=["文件夹","文件","图片","视频","音频","文本","PDF"],o=["mdi-folder","mdi-file","mdi-image","mdi-video","mdi-music-circle","mdi-format-color-text","mdi-file-pdf-box"],l={data:function(){return{TYPE:r,TYPE_ICON:o,TimeUtil:s["a"],headers:[{text:"文件名",sortable:!1,value:"name"},{text:"类型",sortable:!1,value:"type"},{text:"是否加密",sortable:!1,value:"hasKey"},{text:"是否过期",sortable:!1,value:"expirationTime"},{text:"浏览量",sortable:!1,value:"viewCount"},{text:"操作",value:"actions",sortable:!1}],fileList:[],size:20,page:1,totalCount:0,totalPages:0,sysIp:"",message:"",snackbar:!1,showDeletePower:!1,nowDeleteItem:{},deleteDialog:!1}},created:function(){var t=parseInt(this.$route.query.page);isNaN(t)||(this.page=t<=0?1:t),this.getShareList(),this.getSysIp(),this.getPower()},methods:{getSysIp:function(){var t=this;this.httpGet("/ip/sys",(function(e){t.sysIp=e.data}))},getPower:function(){var t=this;this.httpGet("/reader/hasAdmin",(function(e){t.showDeletePower=e.data}))},getShareList:function(){var t=this;this.httpGet("/share/list?page=".concat(this.page,"&size=").concat(this.size),(function(e){t.fileList=e.data.data||[],t.totalCount=e.data.totalCount,t.totalPages=e.data.totalPages}))},checkExpirationTime:function(t){return 0==t.expirationTime?"未设置期限":(new Date).getTime()>t.expirationTime?"已过期":"未过期"},pageChange:function(t){this.page=t,this.$router.push({path:this.$router.path,query:{page:t}}),this.getShareList(),this.$vuetify.goTo(0)},seeFile:function(t){console.log(t),this.$router.push("/share/link/".concat(t.id))},copyLink:function(t){var e="链接：";80==location.port||443==location.port?e+="".concat(this.sysIp,"/share/link/").concat(t.id):e+="".concat(this.sysIp,":").concat(location.port,"/share/link/").concat(t.id),t.hasKey&&(e+="  提取码："+t.key);var a=document.createElement("input");document.body.appendChild(a),a.value=e,a.focus(),a.select(),document.execCommand("copy")&&document.execCommand("copy"),a.blur(),this.message="复制成功！ ",this.snackbar=!0,document.body.removeChild(a)},deleteShare:function(t){this.nowDeleteItem=t,this.deleteDialog=!0},sendDeleteItem:function(){var t=this;this.httpPost("/share/delete",this.nowDeleteItem,(function(e){200===e.status?(t.message="删除成功！",t.snackbar=!0,t.getShareList(),t.deleteDialog=!1):(t.message=e.message,t.snackbar=!0)}))}}},c=l,u=a("2877"),h=a("6544"),p=a.n(h),v=a("8336"),d=a("b0af"),g=a("99d9"),f=a("cc20"),m=a("62ad"),b=a("a523"),y=a("8fea"),_=a("169a"),x=a("132d"),k=a("2909"),w=a("5530"),I=(a("a9e3"),a("d3b7"),a("25f0"),a("d81d"),a("17b3"),a("9d26")),$=a("dc22"),L=a("a9ad"),T=a("de2c"),C=a("7560"),D=a("58df"),S=Object(D["a"])(L["a"],Object(T["a"])({onVisible:["init"]}),C["a"]).extend({name:"v-pagination",directives:{Resize:$["a"]},props:{circle:Boolean,disabled:Boolean,length:{type:Number,default:0,validator:function(t){return t%1===0}},nextIcon:{type:String,default:"$next"},prevIcon:{type:String,default:"$prev"},totalVisible:[Number,String],value:{type:Number,default:0},pageAriaLabel:{type:String,default:"$vuetify.pagination.ariaLabel.page"},currentPageAriaLabel:{type:String,default:"$vuetify.pagination.ariaLabel.currentPage"},previousAriaLabel:{type:String,default:"$vuetify.pagination.ariaLabel.previous"},nextAriaLabel:{type:String,default:"$vuetify.pagination.ariaLabel.next"},wrapperAriaLabel:{type:String,default:"$vuetify.pagination.ariaLabel.wrapper"}},data:function(){return{maxButtons:0,selected:null}},computed:{classes:function(){return Object(w["a"])({"v-pagination":!0,"v-pagination--circle":this.circle,"v-pagination--disabled":this.disabled},this.themeClasses)},items:function(){var t=parseInt(this.totalVisible,10);if(0===t)return[];var e=Math.min(Math.max(0,t)||this.length,Math.max(0,this.maxButtons)||this.length,this.length);if(this.length<=e)return this.range(1,this.length);var a=e%2===0?1:0,i=Math.floor(e/2),n=this.length-i+1+a;if(this.value>i&&this.value<n){var s=1,r=this.length,o=this.value-i+2,l=this.value+i-2-a,c=o-1===s+1?2:"...",u=l+1===r-1?l+1:"...";return[1,c].concat(Object(k["a"])(this.range(o,l)),[u,this.length])}if(this.value===i){var h=this.value+i-1-a;return[].concat(Object(k["a"])(this.range(1,h)),["...",this.length])}if(this.value===n){var p=this.value-i+1;return[1,"..."].concat(Object(k["a"])(this.range(p,this.length)))}return[].concat(Object(k["a"])(this.range(1,i)),["..."],Object(k["a"])(this.range(n,this.length)))}},watch:{value:function(){this.init()}},mounted:function(){this.init()},methods:{init:function(){var t=this;this.selected=null,this.$nextTick(this.onResize),setTimeout((function(){return t.selected=t.value}),100)},onResize:function(){var t=this.$el&&this.$el.parentElement?this.$el.parentElement.clientWidth:window.innerWidth;this.maxButtons=Math.floor((t-96)/42)},next:function(t){t.preventDefault(),this.$emit("input",this.value+1),this.$emit("next")},previous:function(t){t.preventDefault(),this.$emit("input",this.value-1),this.$emit("previous")},range:function(t,e){var a=[];t=t>0?t:1;for(var i=t;i<=e;i++)a.push(i);return a},genIcon:function(t,e,a,i,n){return t("li",[t("button",{staticClass:"v-pagination__navigation",class:{"v-pagination__navigation--disabled":a},attrs:{disabled:a,type:"button","aria-label":n},on:a?{}:{click:i}},[t(I["a"],[e])])])},genItem:function(t,e){var a=this,i=e===this.value&&(this.color||"primary"),n=e===this.value,s=n?this.currentPageAriaLabel:this.pageAriaLabel;return t("button",this.setBackgroundColor(i,{staticClass:"v-pagination__item",class:{"v-pagination__item--active":e===this.value},attrs:{type:"button","aria-current":n,"aria-label":this.$vuetify.lang.t(s,e)},on:{click:function(){return a.$emit("input",e)}}}),[e.toString()])},genItems:function(t){var e=this;return this.items.map((function(a,i){return t("li",{key:i},[isNaN(Number(a))?t("span",{class:"v-pagination__more"},[a.toString()]):e.genItem(t,a)])}))},genList:function(t,e){return t("ul",{directives:[{modifiers:{quiet:!0},name:"resize",value:this.onResize}],class:this.classes},e)}},render:function(t){var e=[this.genIcon(t,this.$vuetify.rtl?this.nextIcon:this.prevIcon,this.value<=1,this.previous,this.$vuetify.lang.t(this.previousAriaLabel)),this.genItems(t),this.genIcon(t,this.$vuetify.rtl?this.prevIcon:this.nextIcon,this.value>=this.length,this.next,this.$vuetify.lang.t(this.nextAriaLabel))];return t("nav",{attrs:{role:"navigation","aria-label":this.$vuetify.lang.t(this.wrapperAriaLabel)}},[this.genList(t,e)])}}),P=a("0fd9"),V=a("2db4"),A=a("2fa4"),E=a("3a2f"),O=Object(u["a"])(c,i,n,!1,null,null,null);e["default"]=O.exports;p()(O,{VBtn:v["a"],VCard:d["a"],VCardActions:g["a"],VCardText:g["c"],VCardTitle:g["d"],VChip:f["a"],VCol:m["a"],VContainer:b["a"],VDataTable:y["a"],VDialog:_["a"],VIcon:x["a"],VPagination:S,VRow:P["a"],VSnackbar:V["a"],VSpacer:A["a"],VTooltip:E["a"]})}}]);