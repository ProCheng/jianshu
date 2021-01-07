<template>
  <div class="bigbox">
    <el-row class="demo-avatar demo-basic article" justify="center" align="center">
      <el-col ><br><h1><b>农村的另类奇怪现象</b></h1></el-col>
      <el-col :span="2">
        <el-avatar :size="48" :src="articleInfo.image"></el-avatar>
      </el-col>
      <el-col :span="10">
        <div class="headline">
          <div class="author">
            {{articleInfo.author}}
            <el-button class="btn_rigister" size="mini" round>关注</el-button>
          </div>
          <span class="iconfont icon-iconset0219 zuan">{{articleInfo.number}}</span>
          <div class="authorInfo">  {{articleInfo.authorInfo.time}}  字数{{articleInfo.authorInfo.numberwords}}  阅读{{articleInfo.authorInfo.read}}</div>
        </div>
      </el-col>
      <el-col :span="24"><br><br>
          <span class="text">{{articleInfo.text}}</span>
      </el-col>
      <el-col :span="24" class="good"><br><br>
        <el-button class="iconfont icon-zan" circle></el-button>
        {{articleInfo.people}}人点赞>
        <el-button class="iconfont icon-cai" circle></el-button>
        <div class=" book">
          <span class="iconfont icon-jishiben"> 记事本</span>
          <el-button class="rests" circle><b>···</b></el-button>
        </div>
      </el-col>
    </el-row>
  </div>
</template>
<script>
export default {
  data() {
    return {
      articleInfo: {
        image: require('@/assets/image/banner-s-daily-e6f6601abc495573ad37f2532468186f.png'),
        author: '张三',
        number: '195',
        people: '686',
        text: '中秋节回家看望父母，顺便去探望了小姨。',
        authorInfo: {
          time: '2020.10.06 22:03:07',
          numberwords: 1077,
          read: 38952
        }
      }
    }
  }
}
</script>
<style lang="less" scoped>
.bigbox{
  padding: 0 0 30px 0;
  margin: 10px 0;
  background-color: #fff;
  width: 100%;
  .article{
    margin: 0px 30px 0 30px;
  }
  .btn_rigister{
    border: 1px solid rgba(236,97,73,.7);
    color: #ea6f5a;
    margin: 0 5px;
  }
  .zuan{
    float: left;
    font-size: 12px;
    color: #ea6f5a;
    margin: 3px 0;
  }
  .author{
    font-size: 16px;
    color: #333;
  }
  .authorInfo{
    font-size: 12px;
    color: #969696;
    padding: 0 40px;
    margin: 3px 0;
  }
  .text{
    font-size: 16px;
  }
  .good{
    font-size: 14px;
    color: #969696;
  }
  .book{
    float: right;
  }
  .rests{
    margin: 0 10px;
  }
}
</style>
