<template>
  <header class="homepage_head">
     <div class="head">
        <el-row type="flex" justify="center" class="that" align="middle">
          <el-col :span="7"><el-input style="width: 100%;" class="search"
            placeholder="写下你的评论">
          </el-input></el-col>
          <el-col :offset="1" :span="2"><span class="iconfont icon-pinglun2"> 评论{{articleInfo.comment}}</span></el-col>
          <el-col :span="2"><span class="iconfont icon-zan1"> 赞{{articleInfo.people}}</span></el-col>
          <b>···</b>
        </el-row>
     </div>
   </header>
</template>
<script>
export default {
  data() {
    return {
      articleInfo: {
        comment: '85',
        people: '686'
      }
    }
  }
}
</script>
<style lang="less" scoped>
.homepage_head{
  position: absolute;
  bottom: 0px;
  width: 100%;
  border-top: 0.5px solid #f0f0f0;
  border-bottom: 0.5px solid #f0f0f0;
  box-shadow: 0 -2px 10px rgba(0,0,0,.05);
  .head{
    background: #FFF;
    padding: 10px 0;
    .search /deep/ input {
      border-radius:40px;border:none;background:#eee
    }
  }
}
</style>
