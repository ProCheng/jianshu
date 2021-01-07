<template>
   <div>
     <el-card :body-style="{ padding: '20px'}" shadow='shadow' class="box-card" v-for="(item,index) in list" :key="index">
       <el-row type="flex" justify="space-between" >
           <el-col :span="item.image==''?24:19">
              <div class="text item">
                <el-link class="title">{{item.title}}</el-link>
                <span class="content">{{item.content}}</span>
                <div class="meta">
                  <span class="iconfont icon-iconset0219"> {{item.reward}}</span>
                  <span>{{item.author}}</span>
                  <span class="iconfont icon-pinglun"> {{item.message}}</span>
                  <span class="iconfont icon-xihuan"> {{item.attention}}</span>
                </div>
              </div>
           </el-col>
           <el-col :span="4" v-if="item.image!=''">
             <div class="text item spread">
                <el-image style="width: 100px; height: 100px"
                  :src="item.image"></el-image>
              </div>
           </el-col>
       </el-row>
      <el-divider></el-divider>
    </el-card>
    <el-button class="more" type="info" size="medium" round><span>阅读更多</span></el-button>
    <div class="side_tool">
      <p>关于简书 · 联系我们 · 加入我们 · 简书出版 · 品牌与徽标 · 帮助中心 · 合作伙伴 · 涂檬-原创插画社区</p>
      <p>©2012-2020 上海佰集信息科技有限公司 / 简书 / 沪ICP备11018329号-5 / Smrz 沪公网安备31010402002252号</p>
      <p>简书网举报电话：021-34770013 / Fanzha 亲爱的市民朋友，上海警方反诈劝阻电话“962110”系专门针对避免您财产被骗受损而设，请您一旦收到来电，立即接听
        <img :src="require('@/assets/image/zggsrz-5695587dccf490ca3e651f4228f7479e.png')" alt="" style="height:25px;width: 20px">
      </p>
    </div>
   </div>
</template>
<script>
export default {
  data() {
    return {
      list: [{
        title: '1111感觉坚持不下去了',
        content: '一开始进简书，什么杂念也没有，只想当成写日记每天闹着玩儿。 可没多久，发现多少有点收益，就萌生了一种念头：我要靠自己的烂笔头创造一点收入，于是乎...',
        author: '乡下张三姐',
        reward: '21.0',
        message: 96,
        attention: 223,
        image: require('@/assets/image/1179389-f501d16f552d5359.png')
      }, {
        title: '感觉坚持不下去了',
        content: '一开始进简书，什么杂念也没有，只想当成写日记每天闹着玩儿。 可没多久，发现多少有点收益，就萌生了一种念头：我要靠自己的烂笔头创造一点收入，于是乎...',
        author: '乡下张三姐',
        reward: '21.0',
        message: 96,
        attention: 223,
        image: require('@/assets/image/1179389-f501d16f552d5359.png')
      }, {
        title: '感觉坚持不下去了',
        content: '一开始进简书，什么杂念也没有，只想当成写日记每天闹着玩儿。 可没多久，发现多少有点收益，就萌生了一种念头：我要靠自己的烂笔头创造一点收入，于是乎...',
        author: '乡下张三姐',
        reward: '21.0',
        message: 96,
        attention: 223,
        image: require('@/assets/image/1179389-f501d16f552d5359.png')
      }, {
        title: '感觉坚持不下去了',
        content: '一开始进简书，什么杂念也没有，只想当成写日记每天闹着玩儿。 可没多久，发现多少有点收益，就萌生了一种念头：我要靠自己的烂笔头创造一点收入，于是乎...',
        author: '乡下张三姐',
        reward: '21.0',
        message: 96,
        attention: 223,
        image: require('@/assets/image/1179389-f501d16f552d5359.png')
      }, {
        title: '感觉坚持不下去了',
        content: '一开始进简书，什么杂念也没有，只想当成写日记每天闹着玩儿。 可没多久，发现多少有点收益，就萌生了一种念头：我要靠自己的烂笔头创造一点收入，于是乎...',
        author: '乡下张三姐',
        reward: '21.0',
        message: 96,
        attention: 223,
        image: require('@/assets/image/1179389-f501d16f552d5359.png')
      }, {
        title: '感觉坚持不下去了',
        content: '一开始进简书，什么杂念也没有，只想当成写日记每天闹着玩儿。 可没多久，发现多少有点收益，就萌生了一种念头：我要靠自己的烂笔头创造一点收入，于是乎...',
        author: '乡下张三姐',
        reward: '21.0',
        message: 96,
        attention: 223,
        image: require('@/assets/image/1179389-f501d16f552d5359.png')
      }, {
        title: '感觉坚持不下去了',
        content: '一开始进简书，什么杂念也没有，只想当成写日记每天闹着玩儿。 可没多久，发现多少有点收益，就萌生了一种念头：我要靠自己的烂笔头创造一点收入，于是乎...',
        author: '乡下张三姐',
        reward: '21.0',
        message: 96,
        attention: 223,
        image: require('@/assets/image/1179389-f501d16f552d5359.png')
      }]
    }
  }
}
</script>
<style lang="less" scoped>
.el-card{
  border: none;
  .title{
    font-size: 18px;
    font-weight: 600;
    color: #000;
  }
  .content{
    margin: 10px 0;
    font-size: 13px;
    color: #999;
    display: inline-block;
  }
  .el-divider{
    margin: 15px 15px 0 0;
    background-color: #f0f0f0;
  }
  .meta>span{
    font-size: 12px;
    margin: 0 5px;
    color: #b4b4b4;
  }
  .meta>span:first-child{
    color: #ea6f5a !important;
  }
}
.more{
  height: 40px;
  width: 100%;
}
.side_tool{
  margin-top: 70px;
  text-align: center;
  font-size: 12px;
  color: #c8c8c8;
  &>p:first-child{
    font-size: 13px;
    color: #a19f9f;
  }
  >p{
    display: inline-block;
    img{
      vertical-align: middle;
    }
  }
}
</style>
