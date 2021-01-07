<template>
   <div class="article_list">
    <el-tabs>
      <el-tab-pane>
         <span slot="label"><i class="iconfont icon-wenzhang-copy"></i> 文章</span>
      </el-tab-pane>
      <el-tab-pane>
        <span class="lable" slot="label"><i class="iconfont icon-dongtai"></i> 动态</span>
      </el-tab-pane>
      <el-tab-pane>
        <span class="lable" slot="label"><i class="iconfont icon-zuixinpinglun"></i> 最新评论</span>
      </el-tab-pane>
      <el-tab-pane>
        <span class="lable" slot="label"><i class="iconfont icon-remen-copy-copy"></i> 热门</span>
      </el-tab-pane>
    </el-tabs>
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
.article_list /deep/.el-tabs__header{
  z-index: 0;
}
.el-tabs{
  box-sizing: border-box;
  padding: 20px;
}
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
</style>
