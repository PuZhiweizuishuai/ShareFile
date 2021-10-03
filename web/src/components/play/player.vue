<template>
<div>
    <div id="dplayer" ref="dplayer" />
</div>
</template>

<script>
import DPlayer from "dplayer";
export default {
  name: "Play",
  props: {
    video: {
      type: Object,
      default: () => {},
    },
  },
  data() {
    return {
      videoData: this.video,
    };
  },
  mounted() {
    this.init();
  },
  methods: {
    init() {
      new DPlayer({
        container: document.querySelector("#dplayer"),
        lang: "zh-cn",
        screenshot: true,
        video: {
          url: `/api/file/share?id=${encodeURIComponent(
            this.videoData.id
          )}&type=inline`, //this.videoData.fileUrl // 'https://api.dogecloud.com/player/get.mp4?vcode=5ac682e6f8231991&userId=17&ext=.mp4',
        },
        // logo: "/logo.png",
        contextmenu: [
          {
            text: "不挂高数",
            link: "https://www.buguagaoshu.com",
          },
        ],
        bottom: "15%",
        unlimited: true,
      });
    },
  },
};
</script>

<style>
#dplayer {
  height: 500px;
}
</style>
