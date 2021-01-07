<template>
   <el-container>
     <el-header>
       <homepageHead></homepageHead>
      </el-header>
      <el-main>
          <el-row type="flex" justify="center" >
              <el-col :span="15">
                <authorInfo></authorInfo>
              </el-col>
          </el-row>
      </el-main>
      <el-footer>
          <el-row type="flex" justify="center" >
              <el-col :span="15">
                <articleList></articleList>
              </el-col>
          </el-row>
      </el-footer>
   </el-container>
</template>
<script>
import homepageHead from '@/components/home/homepage_head'
import authorInfo from '@/components/user/personal_homepage/author_info'
import articleList from '@/components/user/personal_homepage/article_list'
export default {
  data() {
    return {}
  },
  components: {
    homepageHead,
    authorInfo,
    articleList
  }
}
</script>
<style lang="less" scoped>

</style>
