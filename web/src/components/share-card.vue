<template>
  <v-card>
    <v-card-title> 分享 {{ fileInfo.name }} </v-card-title>
    <v-card-subtitle>
      <!-- 重命名 -->
      <v-row>
        <v-col>
          <v-text-field
            v-model="share.name"
            placeholder="对文件进行重命名"
            label="显示给别人的文件名"
            clearable
          />
        </v-col>
      </v-row>
      <!-- 备注 -->
      <v-row>
        <v-col>
          <v-text-field
            v-model="share.note"
            placeholder="文件备注"
            label="文件备注"
            clearable
          />
        </v-col>
      </v-row>

      <!--  -->
      <v-row>
        <v-col>
          <v-switch
            v-model="share.hasKey"
            :label="`是否启用密码：${showSwitchKey()}`"
          ></v-switch>
        </v-col>
      </v-row>

      <!-- 密码 -->
      <v-row v-show="share.hasKey">
        <v-col cols="8">
          <v-text-field
            v-model="share.key"
            label="访问密码"
            clearable
          />
        </v-col>
        <v-col cols="4">
          <v-btn depressed color="success" @click="randomKey()">
            随机生成密码
          </v-btn>
        </v-col>
      </v-row>

      <!-- 过期时间 -->
      <v-row>
        <v-col style="color: black"> 设置分享过期时间，不填则为永久 </v-col>
      </v-row>
      <v-row>
        <TimeForm @time="getExporationTime" />
      </v-row>
    </v-card-subtitle>

    <v-card-actions>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="green darken-1" text @click="shareFile()"> 分享 </v-btn>
      </v-card-actions>
    </v-card-actions>

    <v-snackbar v-model="snackbar" timeout="3000" top>
      {{ message }}

      <template v-slot:action="{ attrs }">
        <v-btn color="pink" text v-bind="attrs" @click="snackbar = false">
          关闭
        </v-btn>
      </template>
    </v-snackbar>
  </v-card>
</template>

<script>
import TimeForm from "@/components/time-form.vue";

export default {
  components: {
    TimeForm,
  },
  props: {
    file: {
      type: Object,
      default: () => {},
    },
  },
  data() {
    return {
      fileInfo: this.file,
      share: {
        note: "",
        expirationTime: 0,
        hasKey: true,
        key: "",
        path: this.file.path,
        name: this.file.name,
        type: this.file.type,
      },
      snackbar: false,
      message: "",
    };
  },
  methods: {
    shareFile() {
      if (this.share.hasKey) {
        if (
          this.share.key == "" ||
          this.share.key == null ||
          this.share.key < 6
        ) {
          this.message = "密码不能为空且密码长度要大于6";
          this.snackbar = true;
          return;
        }
      }
      if (this.share.expirationTime != 0) {
        if (this.share.expirationTime < new Date().getTime()) {
          this.message = "过期时间必须在当前时间之后";
          this.snackbar = true;
          return;
        }
      }
      this.httpPost("/share/save", this.share, (json) => {
        if (json.status === 200) {
          this.$router.push(`/share/link/${json.data.id}`);
        } else {
          //
          this.message = json.message;
          this.snackbar = true;
        }
      });
    },
    getExporationTime(time) {
      //console.log(time);
      this.share.expirationTime = new Date(time).getTime();
    },
    showSwitchKey() {
      if (this.share.hasKey) {
        return "开";
      }
      return "关";
    },
    randomKey() {
      this.share.key = this.guid().substring(0, 6)
    },
    guid() {
      return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(
        /[xy]/g,
        function (c) {
          var r = (Math.random() * 16) | 0,
            v = c == "x" ? r : (r & 0x3) | 0x8;
          return v.toString(16);
        }
      );
    },
  },
};
</script>

<style>
</style>