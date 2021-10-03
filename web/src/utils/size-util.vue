<script>
function getSizeString(size) {
  size = size / 1024;
  if (size < 1024) {
    return size.toFixed(2) + " KB";
  }
  size = size / 1024
  if (size < 1024) {
    return size.toFixed(2) + " M";
  }
  return (size / 1024).toFixed(2) + " GB";
}

export default {
  getSizeString
};
</script>
