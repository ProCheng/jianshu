<template>
  <header class="homepage_head">
     <div class="head">
        <el-row type="flex" justify="center" class="that" align="middle">
          <el-col :span="2">
            <router-link to="/" class="logo hidden-sm-only hidden-xs-only">
              <el-image style="width: 60px; height: 30px" :src="require('@/assets/image/jianshu.png')"></el-image>
            </router-link>
          </el-col>
          <el-col :span="8">
              <el-row type="flex" justify="center" class="that" align="middle">
                   <el-col :span="7"><a href="#" type="danger" class="iconfont icon-zhinanzhenfaxiandaohangdizhiweizhi chengse"> 首页</a></el-col>
                    <el-col :span="7"><a href="#" class="iconfont icon-shoujixiazai"> 下载APP</a></el-col>
                    <el-col :span="10"><el-input style="width: 100%;" class="search"
                      placeholder="搜索"
                      suffix-icon="el-icon-search">
                    </el-input></el-col>
              </el-row>
          </el-col>
          <el-col :span="10">
          <el-row type="flex" justify="center" class="that" align="middle">
          <el-col :span="6" :offset="8"><a href="#" type="danger" class="Aa">Aa</a></el-col>
          <el-col :span="3" ><a href="#" class="Aa">登录</a></el-col>
          <el-col :span="3" ><el-button class="btn_rigister" round>注册</el-button></el-col>
          <el-col :span="4" class="write_article"><el-button type="danger" class="iconfont icon-Pensyumaobi" round> 写文章</el-button></el-col>
              </el-row>
          </el-col>
        </el-row>
     </div>
   </header>
</template>
<script>
export default {
  data() {
    return {}
  }
}
</script>
<style lang="less" scoped>
.homepage_head{
  width: 100%;
  position: fixed;
  z-index: 1;
  border-top: 0.5px solid #f0f0f0;
  border-bottom: 0.5px solid #f0f0f0;
  .head{
    background: #FFF;
    padding: 10px 0;
    .logo{
      height: 30px;
    }
    .chengse{
      color: #ea6f5a;
    }
    .Aa{
      color: #9c9c9c;
    }
    .search /deep/ input {
      border-radius:40px;border:none;background:#eee
    }
    .btn_rigister{
      border: 1px solid rgba(236,97,73,.7);
      color: #ea6f5a;
    }
    .write_article{
      margin: 0 30px;
    }
  }
}
</style>
