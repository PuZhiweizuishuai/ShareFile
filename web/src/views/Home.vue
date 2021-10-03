<template>
<!-- style="background: url(/api/file/bg)no-repeat;" -->
  <v-app id="inspire" ref="mainApp">
    <v-navigation-drawer
      v-model="drawer"
      :clipped="$vuetify.breakpoint.lgAndUp"
      app
    >
      <v-list dense>
        <router-link v-for="item in items" :key="item.text" :to="item.link">
          <v-list-item link>
            <v-list-item-action>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>
                {{ item.text }}
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </router-link>
      </v-list>

      <v-divider />
      <v-footer absolute style="background-color: transparent;">
        <v-col class="text-center" cols="12">
          <a href="https://www.buguagaoshu.com" target="_blank"
            ><strong>不挂高数</strong></a
          >
          ©2021 - {{ new Date().getFullYear() }} Created by
          <a href="https://vuetifyjs.com/" target="_blank"
            ><strong>Vuetify</strong></a
          >
        </v-col>
      </v-footer>
    </v-navigation-drawer>

    <v-app-bar
      :clipped-left="$vuetify.breakpoint.lgAndUp"
      app
      color="blue darken-3"
      dark
    >
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>

      <v-toolbar-title>{{this.$store.state.webInfo.name}}</v-toolbar-title>
    </v-app-bar>

    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<script>
export default {
  data: () => ({
    drawer: null,
    items: [],
    allItems: [
      { icon: "mdi-file", text: "文件浏览", link: "/", auth: true },
      { icon: "mdi-share", text: "分享列表", link: "/filelist", auth: false },
      { icon: "mdi-text", text: "二维码分享", link: "/qrcode", auth: false },
      { icon: "mdi-wrench", text: "个性化", link: "/setting", auth: true },
      { icon: "mdi-help", text: "关于", link: "/about" , auth: false},
    ],
  }),
  created() {
    this.getPower()
    //document.body.style.background = `url("/api/file/bg")`
    //this.$refs.mainApp.style.backgroundImage = `url("/api/file/bg")`
    //document.querySelector("#inspire").style.backgroundImage = `url('/api/file/bg')`
  },
  mounted() {
    this.setBg()
  },
  methods: {
    getPower() {
      this.httpGet('/reader/hasAdmin', (json)=> {
        this.setItemList(json.data)
      })
    },
    setItemList(auth) {
      if (auth) {
        this.items = this.allItems;
      } else {
        for (let i = 0; i < this.allItems.length; i++) {
          if (!this.allItems[i].auth) {
            this.items.push(this.allItems[i])
          }
        }
      }
    },
    setBg() {
      if (this.$store.state.webInfo.background == '' || this.$store.state.webInfo.background == null) {
        //
      } else {
        console.log("set Bg");
        var app = document.querySelector('#inspire')
        app.style.backgroundImage = 'url(/api/file/bg)'
        app.style.backgroundSize = 'cover'
        app.style.backgroundRepeat = 'no-repeat'
        // background-attachment: fixed;
      }

      if (this.$store.state.webInfo.logo == '' || this.$store.state.webInfo.logo == null) {
        //
      } else {
        console.log("set logo");
        document.querySelector('link[rel="icon"]').href = '/api/file/bg?type=logo'
      } 
    }
  }
};
</script>


<style>
a {
  text-decoration: none;
}

</style>
