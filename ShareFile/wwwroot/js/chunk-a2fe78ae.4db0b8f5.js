(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-a2fe78ae"],{1681:function(t,e,a){},d692:function(t,e,a){"use strict";a.r(e);var o=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("v-container",[a("v-row",[a("v-row",[a("v-col",{staticClass:"text-center"},[a("v-card",{staticClass:"mx-auto",attrs:{"max-width":"500","max-height":"500"}},[t.showCode?a("v-card-title",[t._v("此处生成二维码")]):t._e(),a("canvas",{attrs:{id:"container"}})],1)],1)],1)],1),a("v-row",[a("v-textarea",{attrs:{solo:"",label:"请输入文字内容","auto-grow":""},model:{value:t.text,callback:function(e){t.text=e},expression:"text"}})],1),a("v-row",[a("v-col",{staticClass:"text-center"},[a("v-btn",{attrs:{color:"primary"},on:{click:t.createCode}},[t._v("生成")])],1)],1)],1)},n=[],i=a("d055"),r=a.n(i),s={name:"QrCode",data:function(){return{showCode:!0,text:""}},methods:{createCode:function(){""!==this.text&&(this.showCode=!1,r.a.toCanvas(document.querySelector("#container"),this.text,{errorCorrectionLevel:"H"},(function(t){if(t)throw t})))}}},c=s,u=a("2877"),l=a("6544"),h=a.n(l),d=a("8336"),p=a("b0af"),w=a("99d9"),f=a("62ad"),v=a("a523"),x=a("0fd9"),m=a("5530"),g=(a("a9e3"),a("1681"),a("8654")),C=a("58df"),H=Object(C["a"])(g["a"]),b=H.extend({name:"v-textarea",props:{autoGrow:Boolean,noResize:Boolean,rowHeight:{type:[Number,String],default:24,validator:function(t){return!isNaN(parseFloat(t))}},rows:{type:[Number,String],default:5,validator:function(t){return!isNaN(parseInt(t,10))}}},computed:{classes:function(){return Object(m["a"])({"v-textarea":!0,"v-textarea--auto-grow":this.autoGrow,"v-textarea--no-resize":this.noResizeHandle},g["a"].options.computed.classes.call(this))},noResizeHandle:function(){return this.noResize||this.autoGrow}},watch:{lazyValue:function(){this.autoGrow&&this.$nextTick(this.calculateInputHeight)},rowHeight:function(){this.autoGrow&&this.$nextTick(this.calculateInputHeight)}},mounted:function(){var t=this;setTimeout((function(){t.autoGrow&&t.calculateInputHeight()}),0)},methods:{calculateInputHeight:function(){var t=this.$refs.input;if(t){t.style.height="0";var e=t.scrollHeight,a=parseInt(this.rows,10)*parseFloat(this.rowHeight);t.style.height=Math.max(a,e)+"px"}},genInput:function(){var t=g["a"].options.methods.genInput.call(this);return t.tag="textarea",delete t.data.attrs.type,t.data.attrs.rows=this.rows,t},onInput:function(t){g["a"].options.methods.onInput.call(this,t),this.autoGrow&&this.calculateInputHeight()},onKeyDown:function(t){this.isFocused&&13===t.keyCode&&t.stopPropagation(),this.$emit("keydown",t)}}}),y=Object(u["a"])(c,o,n,!1,null,null,null);e["default"]=y.exports;h()(y,{VBtn:d["a"],VCard:p["a"],VCardTitle:w["d"],VCol:f["a"],VContainer:v["a"],VRow:x["a"],VTextarea:b})}}]);