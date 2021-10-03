<template>
  <div id="aplayer" />
</template>

<script>
import APlayer from 'aplayer'
import 'aplayer/dist/APlayer.min.css'

export default {
  props: {
    voice: {
      type: Object,
      default: () => {}
    }
  },
  data() {
    return {
      ap: null,
      vioceData: this.voice
    }
  },
  mounted() {
    this.init()
  },
  methods: {
    init() {
      this.ap = new APlayer({
        container: document.querySelector('#aplayer'),
        audio: [{
          name: this.vioceData.name,
          url: `/api/file/share?id=${encodeURIComponent(this.vioceData.id)}&type=inline`
        }]
      })
    },
    destroy() {
      this.ap.destroy()
    }
  }
}
</script>

<style>

</style>
