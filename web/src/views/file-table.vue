<template>
  <div>
    <v-row>
      <v-col>
        使用提示：使用 CTRL+F 快捷键对当前文件夹进行搜索
      </v-col>
    </v-row>
  
    <v-row>
      <v-col>
        <v-btn text color="success" depressed @click="backToLast()">
          ◀ 返回上一级
        </v-btn>


        <v-btn text style="float:right;" color="warning" depressed @click="onlySeeVideo()">
          只看视频
        </v-btn>
       
        <v-btn text style="float:right;" color="primary" depressed @click="onlySeePhoto()">
          只看图片
        </v-btn>
       
        <v-btn text style="float:right;" color="error" depressed @click="seeAll()">
          全部
        </v-btn>
      </v-col>
    </v-row>

    <v-row justify="end" v-show="isOnlySeePhoto">
        <v-btn text color="error" depressed @click="openViewImage()">
          预览
        </v-btn>
        &nbsp;&nbsp;&nbsp;
    </v-row>


    <v-row> 
      <v-col>
        <v-data-table
          :headers="headers"
          :items="fileList"
          :items-per-page="size"
          hide-default-footer
        >
          <template v-slot:item.name="{ item }">
              <v-icon> {{TYPE_ICON[item.type]}} </v-icon>{{ item.name }}
          </template>
          <template v-slot:item.type="{ item }">
            {{ TYPE[item.type] }}
          </template>
          
          <!-- 显示大小 -->
          <template v-slot:item.size="{ item }">
            {{ showSize(item) }}
          </template>

          <!-- 显示更新时间 -->
          <template v-slot:item.updateTime="{ item }">
            {{ TimeUtil.renderTime(item.updateTime) }}
          </template>

          <template v-slot:item.share="{ item }">
            <v-btn v-if="item.type >= 1" depressed text color="primary" @click="shareFile(item)">
              分享此文件
            </v-btn>
          </template>


          <template v-slot:item.actions="{ item }">
            <a v-if="item.type >= 1" :href="`/api/file/load?path=${encodeURIComponent(item.path)}`" target="_blank">
              <v-icon
                class="mr-2"
                @click="downloadItem(item)"
              >
                mdi-download
              </v-icon>
            </a>
            <v-tooltip v-if="item.type == 0" left>
              <template v-slot:activator="{ on, attrs }">
                <v-btn
                  icon
                  class="mr-2"
                  v-bind="attrs"
                  v-on="on"
                  @click="openFolder(item)"
                >
                  <v-icon>mdi-folder-open</v-icon>
                </v-btn>

              </template>
              <span>打开</span>
            </v-tooltip>

            <a v-if="item.type >= 2" :href="`/api/file/load?path=${encodeURIComponent(item.path)}&type=inline`" target="_blank">
              <v-icon
                class="mr-2"
                @click="downloadItem(item)"
              >
                mdi-eye
              </v-icon>
            </a>

          </template>
        </v-data-table>
      </v-col>
    </v-row>

    <!-- 显示分享弹框 -->
    <v-dialog
      v-model="shareDialog"
      width="800"
      :key="shareDialogkey"
    >
      <ShareCard :file="shareFlieData"/>
    </v-dialog>

    <!-- 显示图片预览框 -->
       <v-dialog
      id="imageViews"
      v-model="imageDialog"
      persistent
      fullscreen
      max-width="290"
    >
      <v-card>
        <v-card-actions>
          <v-spacer />
          <v-btn
            color="error"
            text
            @click="imageDialog = false"
          >
            关闭
          </v-btn>
        </v-card-actions>
        <v-card-text>

          <v-row v-resize="onResize">
            <v-col
              v-touch="{
                left: () => swipe(0),
                right: () => swipe(1),
                up: () => swipe(2),
                down: () => swipe(3)
              }"
              :cols="colsLeft"
            >
              <v-row>
                <v-col cols="6">
                  <span v-text="nowViweImage.name" />
                </v-col>
                <v-col cols="6">
                  <v-switch
                    v-model="showImageList"
                    inset
                    :label="`显示列表`"
                    @click="clickShowImageList()"
                  />
                </v-col>
              </v-row>

              <v-img
                v-if="imageDialog"
                contain
                :max-height="getImageMax()"
                :src="getImageSrc()"
              />
            </v-col>
            <v-col v-if="showImageList" :cols="colsRight">
              <v-card outlined>
                <v-card-text>
                  <v-virtual-scroll
                    :items="fileList"
                    :height="imageListHeight"
                    item-height="64"
                  >
                    <template v-slot:default="{ item, index }">
                      <v-list-item :key="index" link @click="changeImage(item, index)">
                        <v-list-item-content>
                          <v-list-item-title v-text="item.name" />
                        </v-list-item-content>
                      </v-list-item>
                      <v-divider />
                    </template>
                  </v-virtual-scroll>
                </v-card-text>
              </v-card>
            </v-col>
          </v-row>
          <v-col />
          <v-row justify="space-around">
            <v-btn depressed color="primary" @click="previousImage()">
              上一张
            </v-btn>

            <v-btn depressed color="success" @click="nextImage()">
              下一张
            </v-btn>
          </v-row>
          <v-col />
        </v-card-text>

      </v-card>
    </v-dialog>


        <!-- 消息条 -->
    <v-snackbar v-model="snackbar"  timeout="3000" top>
      {{ message }}

      <template v-slot:action="{ attrs }">
        <v-btn color="pink" text v-bind="attrs" @click="snackbar = false">
          Close
        </v-btn>
      </template>
    </v-snackbar>
  </div>
</template>

<script>
import TimeUtil from "@/utils/time-util.vue";
import SizeUtil from "@/utils/size-util.vue";
import ShareCard from "@/components/share-card.vue";


var TYPE = [
  "文件夹",
  "文件",
  "图片",
  "视频",
  "音频",
  "文本",
  "PDF"
]

var TYPE_ICON = [
  "mdi-folder",
  "mdi-file",
  "mdi-image",
  "mdi-video",
  "mdi-music-circle",
  "mdi-format-color-text",
  "mdi-file-pdf-box"
]

export default {
  props: {
    driver: {
      type: String,
      default: "C:/",
    },
  },
  components: {
    ShareCard
  },
  data() {
    return {
      TimeUtil,
      SizeUtil,
      TYPE,
      TYPE_ICON,
      shareDialog: false,
      snackbar: false,
      message: '',
      path: this.driver,
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
          text: "大小",
          sortable: false,
          value: "size",
        },
        {
          text: "更新时间",
          sortable: false,
          value: "updateTime",
        },
        {
          text: "分享",
          sortable: false,
          value: "share",
        },
        { text: "操作", value: "actions", sortable: false },
      ],
      fileList: [],
      backList: [],
      size: 1000,
      // 保存上一次请求成功的地址
      lastPath: '',
      rootPath: this.driver,
      pathHistory: [],
      pathIndex: 0,
      shareFlieData: {},
      shareDialogkey: 56498,
      isOnlySeePhoto: false,
      imageDialog: false,
      nowViweImageIndex: 0,
      nowViweImage: {},
      showImageList: false,
      windowSize: {
        x: 0,
        y: 0
      },
      colsLeft: 10,
      colsRight: 2,
      imageListHeight: window.innerHeight - 100
    };
  },
  created() {
    this.getFileList()
    this.pathHistory.push(this.rootPath)
  },
  methods: {
    getFileList() {
      this.httpGet(`/path/list?path=${encodeURIComponent(this.path)}`, (json) => {
        if (json.status === 200) {
          this.fileList = json.data || [];
          this.size = this.fileList.length
          this.lastPath = this.path
          this.backList = this.fileList
          this.$emit("lastpath", this.lastPath)
        } else if (json.status === 0) {
          this.$emit("errorpath", true)
        }
      });
    },
    backToLast() {
      // TODO 需要重写 Linux 下有 Bug
      if (this.pathHistory.length == 1 || this.pathIndex == 0) {
        this.message = '已经是根目录了，无法再返回！'
        this.snackbar = true
        return
      }
      //this.pathHistory.splice(this.pathIndex, 1)
      this.pathIndex--
      this.pathHistory.pop()
      this.path = this.pathHistory[this.pathIndex]
      
      this.$emit('path', this.path)
      this.getFileList()
    },
    openFolder(item) {
      this.path = item.path
      this.pathHistory.push(item.path)
      this.pathIndex++
      this.getFileList()
      this.$emit('path', this.path)
    },
    showSize(item) {
      if (item.type === 0) {
        return ''
      }
      return SizeUtil.getSizeString(item.size)
    },
    shareFile(item) {
      this.shareDialogkey++
      this.shareFlieData = item
      this.shareDialog = true
    },
    onlySeeVideo() {
      //
      let videList = []
      for (let i = 0; i < this.backList.length; i++) {
        if (this.backList[i].type == 3) {
          videList.push(this.backList[i])
        }
      }
      this.fileList = videList
      this.isOnlySeePhoto = false
    },
    onlySeePhoto() {
      //
      let videList = []
      for (let i = 0; i < this.backList.length; i++) {
        if (this.backList[i].type == 2) {
          videList.push(this.backList[i])
        }
      }
      this.fileList = videList
      this.isOnlySeePhoto = true
    },
    seeAll() {
      //
      this.fileList = this.backList
      this.isOnlySeePhoto = false
    },
    getImageSrc() {
      return `/api/file/load?path=${encodeURIComponent(this.nowViweImage.path)}&type=inline`
    },
    getImageMax() {
      // console.log(window.innerHeight)
      return window.innerHeight - 100
      // return 500
    },
    swipe(type) {
      if (type === 0 || type === 2) {
        this.nextImage()
      } else {
        this.previousImage()
      }
    },
    changeImage(item, index) {
      this.nowViweImageIndex = index
      this.nowViweImage = item
    },
    nextImage() {
      if (this.nowViweImageIndex === this.fileList.length - 1) {
        this.message = '已经是最后一张图片了！'
        this.snackbar = true
        return
      }
      this.nowViweImageIndex++
      this.nowViweImage = this.fileList[this.nowViweImageIndex]
    },
    previousImage() {
      if (this.nowViweImageIndex === 0) {
        this.message = '已经是第一张图片了！'
        this.snackbar = true
        return
      }
      this.nowViweImageIndex--
      this.nowViweImage = this.fileList[this.nowViweImageIndex]
    },
    openViewImage() {
      const length = this.fileList.length
      if (length === 0) {
        this.message = '当前文件夹没有图片。'
        this.snackbar = true
        return
      }
      this.nowViweImage = this.fileList[0]
      this.imageDialog = true
    },
    clickShowImageList() {
      this.onResize()
    },
    onResize() {
      this.windowSize = { x: window.innerWidth, y: window.innerHeight }
      if (this.showImageList) {
        if (this.windowSize.x > 800) {
          this.colsLeft = 10
          this.colsRight = 2
          this.imageListHeight = window.innerHeight - 100
        } else {
          this.colsLeft = 12
          this.colsRight = 12
          this.imageListHeight = 200
        }
      } else {
        this.colsLeft = 12
      }
    },
  },
};
</script>

<style>
</style>