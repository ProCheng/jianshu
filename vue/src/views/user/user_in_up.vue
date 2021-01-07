<template>
   <div class="user_in_up">
     <router-link to="/" class="logo hidden-sm-only hidden-xs-only">
        <el-image style="width: 100px; height: 47px" :src="require('@/assets/image/jianshu.png')"></el-image>
     </router-link>
     <router-view></router-view>
   </div>
</template>
<script>
export default {
  data() {
    return {
    }
  }
}
</script>
<style lang="less" scoped>
.user_in_up{
  display: flex;
  width: 100%;
  height: 100%;
  background: #f1f1f1;
  .logo{
    position: fixed;
    margin: 70px 0 0 70px;
  }
}
</style>
