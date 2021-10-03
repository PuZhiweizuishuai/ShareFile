<template>
  <v-container>
      <v-card>
        <v-card-title>
          获取远程访问权限
        </v-card-title>
        
        <v-card-subtitle>
          <v-row>
            <v-col>
              <v-text-field
                    hide-details
                   
                    solo
                    v-model="key.key"
                    label="访问密码"
                    clearable
                    type="password"
                  />
            </v-col>
          </v-row>

          <v-col/>
          <v-row justify="center">
            <v-btn color="success" @click="sendLogin()">
              登录
            </v-btn>
          </v-row>
          <v-col/>
        </v-card-subtitle>
      </v-card>
          <v-snackbar v-model="snackbar" timeout="3000" top>
      {{ message }}

      <template v-slot:action="{ attrs }">
        <v-btn color="pink" text v-bind="attrs" @click="snackbar = false">
          关闭
        </v-btn>
      </template>
    </v-snackbar>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      key: {
        key: '',
        fileId: ''
      },
      snackbar: false,
      message: ''
    }
  },
  methods: {
    sendLogin() {
      this.httpPost(`/web/login`, this.key, (json) => {
        if (json.status == 200) {
          //
          this.message = '登录成功！'
          this.snackbar = true
          this.$store.state.webInfoStatus = true
          this.$store.state.hasAdmin = true
          sessionStorage.setItem('admin', true)
          console.log("login success!");
          this.$router.push('/')
        } else {
          //
          this.message = '错误！'
          this.snackbar = true
        }
        
      })
    }
  }
}
</script>

<style>

</style>