<template>
  <v-container fluid>
    <!-- 显示路径 -->
    <v-row>
      <v-col>
        <v-card>
          <v-card-subtitle>
            <v-btn
              @click="goToHome()"
              class="mx-2"
              small
              depressed
              fab
              color="warning"
              style="float: left"
            >
              <v-icon dark> mdi-home </v-icon>
            </v-btn>
            <v-text-field
              v-model="path"
              hide-details
              flat
              solo
              prepend-inner-icon="mdi-magnify"
              label="输入完整路径前往相应文件夹如：C:\Windows(按回车前往)"
              @keydown="goToFilePath"
            />
          </v-card-subtitle>
        </v-card>
      </v-col>
    </v-row>
    <!-- 显示盘符 -->
    <div v-show="showDrivers">
      <v-row v-for="item in drivers" :key="item.name">
        <v-col>
          <v-card @click="goToDriver(item)">
            <v-card-title>
              {{ item.name }}
            </v-card-title>
            <v-card-subtitle>
              <v-progress-linear :value="item.percentage" height="25">
                <strong style="color: white"
                  >可用：{{ SizeUtil.getSizeString(item.freeSize) }}，总：{{
                    SizeUtil.getSizeString(item.size)
                  }}</strong
                >
              </v-progress-linear>
            </v-card-subtitle>
          </v-card>
        </v-col>
      </v-row>
    </div>

    <!-- 显示文件夹 -->
    <FileTable
      :key="flushTableKey"
      :driver="path"
      v-if="showDrivers == false"
      @path="nowSetPath"
      @errorpath="flushTableData"
      @lastpath="getLastPath"
    />

    <!-- 消息条 -->
    <v-snackbar v-model="snackbar"  timeout="4000" top>
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
import TimeUtil from "@/utils/time-util.vue";
import SizeUtil from "@/utils/size-util.vue";
import FileTable from "@/views/file-table.vue";

export default {
  components: {
    FileTable,
  },
  data() {
    return {
      drivers: [],
      TimeUtil,
      SizeUtil,
      showDrivers: true,
      path: "",
      flushTableKey: 0,
      lastPath: "",
      snackbar: false,
      message: "",
    };
  },
  created() {
    this.getDrivers();
  },
  methods: {
    getDrivers() {
      this.httpGet("/path/driver", (json) => {
        if (json.status === 200) {
          this.drivers = json.data || [];
        }
      });
    },
    goToFilePath(e) {
      if (e.key === "Enter") {
        if (this.path === "") {
          return;
        }
        this.flushTableKey++;
        this.showDrivers = false;
      }
    },
    flushTableData() {
      console.log("error");
      this.message = "请求路径错误或地址不存在，将为您返回上一次成功访问的地址";
      this.snackbar = true;
      this.flushTableKey++;
      this.path = this.lastPath;
      this.showDrivers = false;
    },
    getLastPath(path) {
      this.lastPath = path;
    },
    nowSetPath(path) {
      this.path = path;
    },
    goToHome() {
      this.showDrivers = true;
      this.path = "";
    },
    goToDriver(item) {
      //this.flushTableKey ++
      this.showDrivers = false;
      this.path = item.name;
      //console.log(item);
    },
  },
};
</script>

<style>
</style>