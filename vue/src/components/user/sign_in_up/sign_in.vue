<template>
   <div class="sign_in">
      <h4 class="title">
        <router-link to="/user_in_up/sign_in">登录</router-link>
        <div>·</div>
        <router-link to="/user_in_up/sign_up">注册</router-link>
      </h4>
      <el-form :model="user" ref="ruleForm" label-width="0px" class="demo-ruleForm">
        <el-form-item prop="account">
          <el-input prefix-icon="iconfont icon-zhanghao" placeholder="手机号或邮箱" clearable v-model.trim="user.account"></el-input>
        </el-form-item>
        <el-form-item prop="pwd">
          <el-input prefix-icon="iconfont icon-mima" placeholder="密码" show-password type="password" clearable v-model.trim="user.pwd"></el-input>
        </el-form-item>
        <el-form-item>
            <el-row type="flex">
                <el-col :span="16">
                   <el-checkbox-group v-model="recall">
                      <el-checkbox label="记住我" name="name" :checked="recall"></el-checkbox>
                    </el-checkbox-group>
                </el-col>
                <el-col :span="8"> <el-link type="info" :underline="false" href="http://www.baidu.com">登录遇到问题</el-link></el-col>
            </el-row>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" class="btn_login" @click="click_login">登录</el-button>
        </el-form-item>
      </el-form>
      <el-divider content-position="center"><span>社交帐号登录</span></el-divider>
      <el-row type="flex" justify="center" class="that">
          <el-col :span="6"><el-link href="#" type="danger" class="iconfont icon-weibo"></el-link></el-col>
          <el-col :span="6"><el-link href="#" type="success" class="iconfont icon-weixin"></el-link></el-col>
          <el-col :span="6"><el-link href="#" type="primary" class="iconfont icon-qq"></el-link></el-col>
      </el-row>
   </div>
</template>
<script>
export default {
  data() {
    return {
      // 用户账号密码
      user: {
        account: '',
        pwd: ''
      },
      // 账号密码验证规则
      rules: {
        account: [{ required: true, message: '请输入', trigger: 'blur' }],
        pwd: [{ required: true, message: '请输入', trigger: 'blur' }]
      },
      recall: false
    }
  },
  watch: {
    // 侦听是否记住密码
    recall: function(recall) {
      if (recall) {
        window.localStorage.setItem('User', JSON.stringify(this.user))
      } else {
        window.localStorage.removeItem('User')
      }
    }
  },
  methods: {
    // 点击登录
    click_login() {
      this.$http.post('api/user/login', this.user).then(res => {
        if (res.state === 200) {
          this.$Notification({ title: '系统提示', message: res.message, type: 'success' })
          window.localStorage.setItem('token', res.token)
          this.$router.push('/')
        } else {
          this.$Notification({ title: '系统提示', message: res.message, type: 'error' })
        }
      })
    }
  },
  mounted () {
    if (window.localStorage.getItem('User')) {
      this.recall = true
      this.user = JSON.parse(window.localStorage.getItem('User'))
    }
  }
}
</script>
<style lang="less" scoped>
.sign_in{
  padding: 30px 50px 30px;
  box-sizing: border-box;
  width: 400px;
  height: 500px;
  margin: auto;
  background-color: #fff;
  box-shadow: 0 0 8px rgba(0,0,0,.1);
  .title{
    display: flex;
    justify-content: center;
    margin-bottom: 40px;
    :nth-child(1){
      color: #ea6f5a;
      border-bottom: 2px solid #ea6f5a;
    }
    :nth-child(2){
      margin: 0 13px;
    }
    *{
      padding: 10px 7px;
      color: #969696;
      font-size: 18px;
      font-weight: 600;
    }
    :first-child:hover,:last-child:hover{
      border-bottom: 2px solid #ea6f5a;
    }
  }
}
.el-form-item{
  margin-bottom: 0px;
}
.el-form-item{
  margin-top: -1px;
}
.el-input{
  color: #ea6f5a !important;
}
.sign_in /deep/.el-input__icon{
    color: #969696;
    font-size: 20px;
    line-height: 50px;
}
.sign_in /deep/.el-input__inner{
    height: 50px;
    border-radius: 0px;
    background-color: hsla(0,0%,71%,.1);
    border: 1px solid #c8c8c8;
}
.sign_in /deep/.el-input__inner:focus,.el-input__inner:hover{
    border-color: #c8c8c8;
}
.btn_login{
  width: 100%;
  padding: 14px;
  border-radius: 20px;
  background-color: #3194d0;
  font-size: 20px;
}
.el-divider{
  width: 95%;
  margin: auto;
  margin-top: 50px;
  span{
    font-size: 10px;
    color: #b5b5b5;
  }
}
.that{
  text-align: center;
  margin-top: 40px;
}
.that .el-link{
  font-size: 32px;
}
</style>
