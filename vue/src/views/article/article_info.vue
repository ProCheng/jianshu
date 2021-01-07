<template>
   <div class="box">
      <el-row type="flex" justify="center" >
        <el-col :span="13">
          <articleInfo></articleInfo>
        </el-col>
      </el-row>
      <writeComment></writeComment>
   </div>
</template>
<script>
import articleInfo from '@/components/article/article_info/article'
import writeComment from '@/components/article/article_info/write_comment'
export default {
  data() {
    return {}
  },
  components: {
    articleInfo,
    writeComment
  }
}
</script>
<style lang="less" scoped>
.box{
  background:rgb(245, 244, 244);
  height: 100%;
}
</style>
