<template>
   <div>
       <el-row type="flex" justify="center" align="middle" >
           <el-col :span="4">
             <el-avatar :size='80'></el-avatar>
           </el-col>
           <el-col :span="10" :pull="1">
             <div class="authorName">{{authorInfo.name}} <i :class="authorInfo.sex==0?'iconfont icon-nv icon_nv':'iconfont icon-nan icon_nan'"></i></div>
             <div class="item">
               <span>{{authorInfo.attention}} </span><br>
               <a href="#">关注 <i class="iconfont icon-dayuhao"></i></a>
             </div>
             <div class="item">
               <span>{{authorInfo.articleCount}} </span><br>
               <a href="#">文章 <i class="iconfont icon-dayuhao"></i></a>
             </div>
           </el-col>
           <el-col :span="10">
             <el-button round> 发简信</el-button>
             <el-button type="primary" class="iconfont icon-jiahao" round> 关注</el-button>
           </el-col>
       </el-row>
   </div>
</template>
<script>
export default {
  data() {
    return {
      authorInfo: {
        name: '水月圣印',
        sex: 0,
        attention: 12,
        articleCount: 400
      }
    }
  }
}
</script>
<style lang="less" scoped>
.authorName{
  margin-bottom: 10px;
  font-size: 21px;
  font-weight: 700;
}
.item{
  display: inline-block;
  margin: 0 15px 0 0;
  font-size: 15px;
  color: #333;
  a:last-child,a:last-child i{
    color: #969696;
    font-size: 12px;
  }
}
.icon_nv{
  color: #ea6f5a;
}
.icon_nan{
  color: #195be9;
}
</style>
