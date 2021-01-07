<template>
  <el-container>
    <el-header>
          <homepageHead class="homepageHead"></homepageHead>
    </el-header>
    <el-main>
      <el-row type="flex" justify="center" >
         <el-col :span="12" :xs="24">
            <articleList></articleList>
         </el-col>
         <el-col :span="4" class="hidden-xs-only">
          <articleSide></articleSide>
        </el-col>
      </el-row>
    </el-main>
  </el-container>
</template>
<script>
import homepageHead from '@/components/home/homepage_head.vue'
import articleList from '@/components/home/article_list'
import articleSide from '@/components/home/article_side'
export default {
  data() {
    return {}
  },
  components: {
    articleList,
    articleSide,
    homepageHead
  }
}
</script>
<style lang="less" scoped>

</style>
