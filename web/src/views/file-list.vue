<template>
  <v-container>
    <v-row>
      <v-col>
        <h3>所有分享</h3>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-data-table
          :headers="headers"
          :items="fileList"
          :items-per-page="size"
          :page.sync="page"
          hide-default-footer
        >
          <template v-slot:item.name="{ item }">
            <v-icon> {{ TYPE_ICON[item.type] }} </v-icon>{{ item.name }}
          </template>
          <template v-slot:item.type="{ item }">
            {{ TYPE[item.type] }}
          </template>

          <template v-slot:item.expirationTime="{ item }">
            <!-- {{ checkExpirationTime(item) }} -->
            <v-tooltip top v-if="item.expirationTime != 0">
              <template v-slot:activator="{ on, attrs }">
                <v-chip color="primary" dark v-bind="attrs" v-on="on">
                  {{ checkExpirationTime(item) }}
                </v-chip>
              </template>
              <span>
                过期时间： {{ TimeUtil.renderTime(item.expirationTime) }}
              </span>
            </v-tooltip>
            <span v-if="item.expirationTime == 0">
              {{ checkExpirationTime(item) }}
            </span>
          </template>

          <template v-slot:item.hasKey="{ item }">
             {{ item.hasKey ? '是' : '否' }}
          </template>

          <template v-slot:item.actions="{ item }">
            <v-btn depressed color="primary" small @click="seeFile(item)"> 查看 </v-btn>
            &nbsp;&nbsp;
            <v-btn depressed color="success" small @click="copyLink(item)"> 复制链接 </v-btn>
          </template>
        </v-data-table>
      </v-col>
    </v-row>
    <v-col></v-col>
    <v-row justify="center">
     
         <v-pagination v-model="page" :length="totalPages" circle :total-visible="10" @input="pageChange" />

    </v-row>
    <v-col></v-col>
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
import TimeUtil from "@/utils/time-util.vue";
var TYPE = ["文件夹", "文件", "图片", "视频", "音频", "文本", "PDF"];

var TYPE_ICON = [
  "mdi-folder",
  "mdi-file",
  "mdi-image",
  "mdi-video",
  "mdi-music-circle",
  "mdi-format-color-text",
  "mdi-file-pdf-box",
];
export default {
  data() {
    return {
      TYPE,
      TYPE_ICON,
      TimeUtil,
      headers: [
        {
          text: "文件名",
          sortable: false,
          value: "name",
        },
        {
          text: "类型",
          sortable: false,
          value: "type",
        },
        {
          text: "是否加密",
          sortable: false,
          value: "hasKey",
        },
        {
          text: "是否过期",
          sortable: false,
          value: "expirationTime",
        },
        {
          text: "浏览量",
          sortable: false,
          value: "viewCount",
        },
        { text: "操作", value: "actions", sortable: false },
      ],
      fileList: [],
      size: 20,
      page: 1,
      totalCount: 0,
      totalPages: 0,
      sysIp: '',
      message: '',
      snackbar: false
    };
  },
  created() {
    const page = parseInt(this.$route.query.page)
    if (!isNaN(page)) {
      if (page <= 0) {
        this.page = 1
      } else {
        this.page = page
      }
    }
    this.getShareList();
    this.getSysIp();
  },
  methods: {
    getSysIp() {
      this.httpGet("/ip/sys", (json) => {
        this.sysIp = json.data
      });
    },
    getShareList() {
      this.httpGet(
        `/share/list?page=${this.page}&size=${this.size}`,
        (json) => {
          this.fileList = json.data.data || [];
          this.totalCount = json.data.totalCount
          this.totalPages = json.data.totalPages
        }
      );
    },
    checkExpirationTime(item) {
      if (item.expirationTime == 0) {
        return "未设置期限";
      }
      if (new Date().getTime() > item.expirationTime) {
        return "已过期";
      } else {
        return "未过期";
      }
    },
    pageChange(page) {
      this.page = page
      this.$router.push({
        path: this.$router.path,
        query: { page: page }
      })
      this.getShareList()
      this.$vuetify.goTo(0)
    },
    seeFile(item) {
      console.log(item);
      this.$router.push(`/share/link/${item.id}`)
    },
    copyLink(item) {
      //console.log(item);
      let shareUrl = '链接：'
      if (location.port == 80 || location.port == 443) {
          shareUrl += `${this.sysIp}/share/link/${item.id}`;
        } else {
          shareUrl += `${this.sysIp}:${location.port}/share/link/${item.id}`;
      }
      if (item.hasKey) {
        shareUrl += "  提取码：" + item.key
      }
      // console.log(shareUrl);
      // 链接：http://172.16.2.141:8080/share/link/fac1c96477f948cb89dcf5e7a46e991e  提取码：123456
      const transfer = document.createElement("input");
      document.body.appendChild(transfer);
      transfer.value = shareUrl; // 这里表示想要复制的内容
      transfer.focus();
      transfer.select();
      if (document.execCommand("copy")) {
        document.execCommand("copy");
      }
      transfer.blur();
      this.message = "复制成功！ ";
      this.snackbar = true;
      document.body.removeChild(transfer);
    }
  },
};
</script>
<style>
</style>