<template>
   <div class="sign_up">
      <h4 class="title">
        <router-link to="/user_in_up/sign_in">登录</router-link>
        <div>·</div>
        <router-link to="/user_in_up/sign_up">注册</router-link>
      </h4>
      <el-form :model="user" ref="ruleForm" :rules="rules" label-width="0px" class="demo-ruleForm" :show-message="false">
        <el-form-item prop="account">
          <el-input prefix-icon="iconfont icon-zhanghao" placeholder="你的昵称" clearable v-model.trim="user.account"></el-input>
        </el-form-item>
        <el-form-item prop="phone">
          <el-input prefix-icon="iconfont icon-shoujihaoma" type="email" placeholder="手机号" clearable v-model.trim="user.phone"></el-input>
        </el-form-item>
        <el-form-item prop="valid" v-if="user.phone">
          <el-input prefix-icon="iconfont icon-yanzhengma" placeholder="验证码" v-model.trim="user.valid">
            <template slot="append">
                <el-button type="danger" @click="click_send_valid">{{validTime}}</el-button>
            </template>
          </el-input>
        </el-form-item>
         <el-form-item prop="pwd">
          <el-input prefix-icon="iconfont icon-mima" placeholder="设置密码" show-password type="password" clearable v-model.trim="user.pwd"></el-input>
        </el-form-item>
        <el-form-item class="login">
          <el-button type="success" class="btn_login" @click="click_rigister">注册</el-button>
        </el-form-item>
      </el-form>
      <div class="explain">点击 “注册” 即表示您同意并愿意遵守简书 <br>
        <el-link href="#" type="primary">用户协议</el-link> 和
        <el-link href="#" type="primary">隐私政策</el-link> 。
      </div>
      <el-divider content-position="center"><span>社交帐号直接注册</span></el-divider>
      <el-row type="flex" justify="center" class="that">
          <el-col :span="6"><el-link href="#" type="success" class="iconfont icon-weixin"></el-link></el-col>
          <el-col :span="6"><el-link href="#" type="primary" class="iconfont icon-qq"></el-link></el-col>
      </el-row>
   </div>
</template>
<script>
export default {
  data() {
    return {
      user: {
        account: '',
        phone: '',
        pwd: '',
        valid: ''
      },
      rules: {
        account: [{ required: true, trigger: 'none' }],
        pwd: [{ required: true, trigger: 'none' }],
        phone: [{ required: true, trigger: 'none', pattern: /^[A-Za-z\d]+([-_.][A-Za-z\d]+)*@([A-Za-z\d]+[-.])+[A-Za-z\d]{2,4}$/ }],
        valid: [{ required: true, trigger: 'none' }]
      },
      validTime: '发送'
    }
  },
  methods: {
    // 点击注册
    click_rigister() {
      this.$refs.ruleForm.validate((a, b) => {
        if (!a) {
          this.$Message({ message: '请写填完整或者邮箱有误', type: 'error' })
          return false
        }
        this.$http.post('api/user/Register/' + window.sessionStorage.getItem('validId') + '/' + this.user.valid, {
          Account: this.user.account,
          pwd: this.user.pwd,
          NikeName: this.user.account
        }).then(res => {
          if (res.state === 200) {
            this.$Message({ message: res.message, type: 'success' })
            this.$router.push('/user/sign_in')
          } else {
            this.$Message({ message: res.message, type: 'error' })
          }
        })
      })
    },
    // 发送验证码
    async click_send_valid() {
      const reg = /^[A-Za-z\d]+([-_.][A-Za-z\d]+)*@([A-Za-z\d]+[-.])+[A-Za-z\d]{2,4}$/
      if (!reg.test(this.user.phone)) {
        this.$Message({
          type: 'error',
          message: '邮箱有误'
        })
        return
      }
      if (this.validTime === '发送') {
        this.validTime = 59
        var time = setInterval(() => {
          this.validTime--
          if (this.validTime <= 0) {
            clearInterval(time)
            this.validTime = '发送'
          }
        }, 1000)
        await this.$http.get('api/Valid/Email', {
          params: { email: this.user.phone }
        }).then(res => {
          if (res.state === 200) {
            this.$Notification({
              type: 'success',
              title: '系统提示',
              message: res.message
            })
            // 将验证码id存入session storage中
            window.sessionStorage.setItem('validId', res.validId)
          } else {
            clearInterval(time)
            this.validTime = '发送'
            this.$Notification({
              type: 'error',
              title: '系统提示',
              message: res.message
            })
          }
        })
      }
    }
  }
}
</script>
<style lang="less" scoped>
.sign_up{
  padding: 30px 50px 30px;
  box-sizing: border-box;
  width: 400px;
  height: 600px;
  margin: auto;
  background-color: #fff;
  box-shadow: 0 0 8px rgba(0,0,0,.1);
  .title{
    display: flex;
    justify-content: center;
    margin-bottom: 40px;
    :nth-child(3){
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
.el-input{
  color: #ea6f5a !important;
}
.btn_login{
  width: 100%;
  padding: 14px;
  border-radius: 20px;
  font-size: 20px;
}
.el-divider{
  width: 95%;
  margin: auto;
  margin-top: 40px;
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
.login{
  margin-top: 20px;
}
.explain{
  color: #969696;
  font-size: 12px;
  width: 90%;
  margin: 25px auto;
  text-align: center;
  .el-link{
    font-size: 12px;
  }
}
 .sign_up /deep/.el-input__icon{
    color: #969696;
    font-size: 20px;
    line-height: 50px;
}
.sign_up /deep/.el-input__inner{
    height: 50px;
    border-radius: 0px;
    background-color: hsla(0,0%,71%,.1);
    border: 1px solid #c8c8c8;
    border-top: none;
}
.sign_up .el-form-item:first-child{
  /deep/.el-input__inner{
    border-top: 1px solid #c8c8c8;
}
}
.sign_up /deep/.el-input__inner:focus,.el-input__inner:hover{
    border-color: #c8c8c8;
}

</style>
