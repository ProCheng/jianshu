<template>
   <div>
     <div class="board">
       <div v-for="(item, index) in list.board" :key="index">
         <el-image
         style="width: 100%; height: 50px"
         :src="item"></el-image>
       </div>
     </div>
     <div class="recommendedAuthors">
         <el-row type="flex" justify="space-between" class="title">
             <el-col :span="8"><span>推荐作者</span></el-col>
             <el-col :span="7"><span class="iconfont icon-shuaxin"> 换一批</span></el-col>
         </el-row>
          <el-row class="authoritem" type="flex" justify="center" align="middle" v-for="(item, index) in list.recommendedAuthors" :key="index" >
              <el-col :span="5">
                <el-avatar :size="48" :src="item.image"></el-avatar>
              </el-col>
              <el-col :span="13" :offset="1">
                <div class="author">{{item.author}}</div>
                <div class="authorInfo">写了{{item.authorInfo.wordCount}} · {{item.authorInfo.love}}喜欢</div>
              </el-col>
              <el-col :span="6"><span class="attention iconfont icon-jiahao"> 关注</span></el-col>
          </el-row>
     </div>
   </div>
</template>
<script>
export default {
  data() {
    return {
      list: {
        board: [
          require('@/assets/image/banner-s-5-4ba25cf5041931a0ed2062828b4064cb.png'),
          require('@/assets/image/banner-s-7-1a0222c91694a1f38e610be4bf9669be.png'),
          require('@/assets/image/banner-s-club-aa8bdf19f8cf729a759da42e4a96f366.png'),
          require('@/assets/image/banner-s-daily-e6f6601abc495573ad37f2532468186f.png')
        ],
        recommendedAuthors: [
          {
            image: require('@/assets/image/banner-s-daily-e6f6601abc495573ad37f2532468186f.png'),
            author: '张三',
            authorInfo: {
              wordCount: 1209,
              love: 5.4
            }
          },
          {
            image: require('@/assets/image/banner-s-daily-e6f6601abc495573ad37f2532468186f.png'),
            author: '张三',
            authorInfo: {
              wordCount: 1209,
              love: 5.4
            }
          },
          {
            image: require('@/assets/image/banner-s-daily-e6f6601abc495573ad37f2532468186f.png'),
            author: '张三',
            authorInfo: {
              wordCount: 1209,
              love: 5.4
            }
          }
        ]
      }
    }
  }
}
</script>
<style lang="less" scoped>
.recommendedAuthors{
  margin: 20px 0;
  .title{
    font-size: 14px;
    color: #969696;
  }
  .authoritem{
    margin: 10px 0;
     .author{
    font-size: 14px;
    color: #333;
    }
    .authorInfo{
      font-size: 12px;
      color: #969696;
    }
    .attention{
      font-size: 13px;
      color: #42c02e;
      font-weight: 600;
    }
  }
}
</style>
