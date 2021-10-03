<template>
  <v-container>
    <v-card>
      <v-card-title>
        <v-row>
          <v-col>个性化设置</v-col>
        </v-row>
      </v-card-title>

      <v-card-subtitle>
        <!-- 网站名 -->
        <v-row>
          <v-col>
            <v-text-field
              label="网站名"
              v-model="webInfo.name"
              :rules="[rules.required]"
            ></v-text-field>
          </v-col>
        </v-row>

        <!-- 域名 -->
        <v-row>
          <v-col>
            <v-text-field
              label="域名，如果没有则为空"
              v-model="webInfo.baseUrl"
            ></v-text-field>
          </v-col>
        </v-row>

        <!-- 背景图 -->
        <v-row>
          <v-col>
            <v-text-field
              label="背景图，填入本地图片路径，如D:\Pictures\background.jpg"
              v-model="webInfo.background"
            ></v-text-field>
          </v-col>
        </v-row>

        <!-- logo -->
        <v-row>
          <v-col>
            <v-text-field
              label="logo，填入本地图片路径，如D:\Pictures\logo.jpg"
              v-model="webInfo.logo"
            ></v-text-field>
          </v-col>
        </v-row>

        <!-- 背景图 -->
        <v-row>
          <v-col>
            <v-text-field
              label="分享页显示文字"
              v-model="webInfo.shareText"
            ></v-text-field>
          </v-col>
        </v-row>

        <v-col />

        <v-row justify="center">
    
            <v-btn color="success" depressed @click="updateInfo()">
              更新
            </v-btn>
      
        </v-row>

        <v-col />
      </v-card-subtitle>
    </v-card>

    <v-snackbar v-model="snackbar" timeout="3000" top>
      {{ message }}

      <template v-slot:action="{ attrs }">
        <v-btn color="pink" text v-bind="attrs" @click="snackbar = false">
          Close
        </v-btn>
      </template>
    </v-snackbar>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      webInfo: this.$store.state.webInfo,
      rules: {
        required: (value) => !!value || "网站名不能为空！",
        counter: (value) => value.length <= 20 || "Max 20 characters",
        email: (value) => {
          const pattern =
            /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
          return pattern.test(value) || "Invalid e-mail.";
        },
      },
      snackbar: false,
      message: "",
    };
  },
  methods: {
    updateInfo() {
      console.log(this.webInfo);
      this.httpPost('/web/update', this.webInfo, (json)=>{
        if (json.state == 200) {
          this.message = "修改成功"
          this.snackbar = true
        } else {
          this.message = json.message
          this.snackbar = true
        }
      })
    },
  },
};
</script>

<style>
</style>