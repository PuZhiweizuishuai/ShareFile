<template>
  <v-container>
    <!-- 显示分享信息 -->
    <v-row v-if="showShareInfo">
      <v-col>
        <v-card>
          <v-card-title> 文件 {{ fileInfo.name }} 分享成功 </v-card-title>
          <v-card-subtitle>
            <v-row>
              <v-col> 将此连接分享给你的好友即可访问此文件： </v-col>
            </v-row>

            <v-row>
              <v-col>
                {{ this.shareUrl }}
                <v-btn depressed color="success" @click="copy()">
                  点击复制
                </v-btn>
              </v-col>
            </v-row>
          </v-card-subtitle>
        </v-card>
      </v-col>
    </v-row>
    <div style="height: 64px" />
    <!--显示分享的文件  -->
    <v-row v-if="hasShow">
      <!-- 显示密码 -->
      <v-col v-if="fileInfo.canReader == false">
        <v-card>
          <v-card-title>
            <v-row justify="center">
              <v-col cols="10"> 这个分享是被密码保护的 </v-col>
            </v-row>
          </v-card-title>
          <v-card-subtitle>
            <v-col></v-col>
            <v-row justify="center">
              <v-col cols="10">
                <v-card outlined>
                  <v-text-field
                    hide-details
                    flat
                    solo
                    v-model="share.key"
                    label="访问密码"
                    clearable
                    type="password"
                  />
                </v-card>
              </v-col>
            </v-row>
            <v-row justify="center">
              <v-btn depressed color="success" @click="sendkey()"> 确定 </v-btn>
            </v-row>
          </v-card-subtitle>
          <v-card-actions> </v-card-actions>
        </v-card>
      </v-col>

      <!-- 显示文件 -->
      <v-col v-if="fileInfo.canReader">
        <v-card>
          <v-card-title>
            <v-row cols="10" justify="center">
              <v-col>
                {{ fileInfo.name }}
              </v-col>
            </v-row>
          </v-card-title>
          <v-card-text>
            <v-row cols="10" justify="center">
              <v-col>
                {{ fileInfo.note }}
              </v-col>
            </v-row>
            <!-- 预览 -->

            <v-row v-if="fileInfo.type >= 2">
              <v-col>
                <!-- 图片 -->
                <v-img
                  v-if="fileInfo.type == 2"
                  :src="`/api/file/share?id=${encodeURIComponent(
                    this.fileInfo.id
                  )}&type=inline`"
                ></v-img>
                <!-- 视频 -->
                <DPlayer v-if="fileInfo.type == 3" :video="fileInfo" />
                <!-- 音乐 -->
                <Voice v-if="fileInfo.type == 4" :voice="fileInfo" />
                <!-- 文本 -->
                <!-- PDF -->
                <embed
                  v-if="fileInfo.type == 6"
                  :src="`/api/file/share?id=${encodeURIComponent(
                    this.fileInfo.id
                  )}&type=inline`"
                  type="application/pdf"
                  style="width: 100%; height: 600px"
                />
              </v-col>
            </v-row>
             <v-row v-if="fileInfo.type >= 2">
              <v-col>
                如果预览出现异常，请下载后再查看！
              </v-col>
            </v-row>
          </v-card-text>

          <v-card-actions>
            <v-col />
            <v-row justify="center">
              <a
                :href="`/api/file/share?id=${encodeURIComponent(fileInfo.id)}`"
                target="_blank"
              >
                <v-btn color="success" link> 下载 </v-btn>
              </a>
            </v-row>
            <v-col>&nbsp;</v-col>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>

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
var TYPE = ["文件夹", "文件", "图片", "视频", "音频", "文本", "PDF"];
import DPlayer from "@/components/play/player.vue";
import Voice from "@/components/play/voice.vue";
export default {
  components: {
    DPlayer,
    Voice,
  },
  data() {
    return {
      TYPE,
      id: this.$route.params.id,
      fileInfo: {},
      hasShow: false,
      errorMsg: "",
      showShareInfo: false,
      sysIp: "",
      shareUrl: "",
      snackbar: false,
      message: "",
      isShowFile: false,
      share: {
        key: "",
      },
    };
  },
  created() {
    this.getSysIp();
  },
  methods: {
    getShareFile() {
      this.httpGet(`/share/get?id=${this.$route.params.id}`, (json) => {
        if (json.status == 200) {
          this.fileInfo = json.data;
          this.hasShow = true;
          if (this.fileInfo.hasKey) {
            this.isShowFile = false;
          } else {
            this.isShowFile = true;
          }
          this.checkHasShowShareInfo();
        } else {
          this.hasShow = false;
          this.errorMsg = json.message;
        }
      });
    },
    getSysIp() {
      this.httpGet("/ip/sys", (json) => {
        this.sysIp = json.data;
        this.getShareFile();
      });
    },
    checkHasShowShareInfo() {
      if (
        location.hostname == "127.0.0.1" ||
        location.hostname == "localhost" ||
        location.hostname == "[::1]" ||
        this.$store.state.hasAdmin
      ) {
        this.showShareInfo = true;
        if (location.port == 80 || location.port == 443) {
          this.shareUrl = `${this.sysIp}/share/link/${this.$route.params.id}`;
        } else {
          this.shareUrl = `${this.sysIp}:${location.port}/share/link/${this.$route.params.id}`;
        }
      }
    },
    sendkey() {
      //
      this.share.fileId = this.fileInfo.id;
      this.httpPost("/reader/file", this.share, (json) => {
        if (json.status == 200) {
          this.getShareFile();
        } else {
          this.message = "密码错误";
          this.snackbar = true;
        }
      });
    },
    copy() {
      const transfer = document.createElement("input");
      document.body.appendChild(transfer);
      transfer.value = this.shareUrl; // 这里表示想要复制的内容
      transfer.focus();
      transfer.select();
      if (document.execCommand("copy")) {
        document.execCommand("copy");
      }
      transfer.blur();
      this.message = "复制成功！ ";
      this.snackbar = true;
      document.body.removeChild(transfer);
    },
  },
};
</script>

<style>
</style>