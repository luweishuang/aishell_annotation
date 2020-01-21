<template>
  <Form :model="userItem" ref="formValidate" :label-width="100" :rules="userValidate">
    <FormItem label="用户名：" prop="userName" v-if="!userItem.id">
      <Input v-model="userItem.userName" autocomplete="off" placeholder="输入用户名..." v-if="!userItem.id"></Input>
      <span v-if="userItem.id">{{userItem.userName}}</span>
    </FormItem>
      <FormItem label="用户名：" prop="userName" v-else>
      <span >{{userItem.userName}}</span>
    </FormItem>
    <FormItem label="姓名：" prop="realName">
      <Input v-model="userItem.realName" autocomplete="off" placeholder="输入姓名..."></Input>
    </FormItem>
    <FormItem label="密码：" prop="password" v-if="!userItem.id">
      <Input type="password" autocomplete="off" v-model="userItem.password" placeholder="输入密码..."></Input>
    </FormItem>
    <FormItem label="确认密码：" prop="passwordConfirm" v-if="!userItem.id">
      <Input
        type="password"
        autocomplete="off"
        v-model="userItem.passwordConfirm"
        placeholder="确认密码..."
      ></Input>
    </FormItem>
    <FormItem label="性别：">
      <RadioGroup v-model="userItem.sex">
        <Radio label="0">男</Radio>
        <Radio label="1">女</Radio>
      </RadioGroup>
    </FormItem>
    <FormItem label="邮箱：" prop="email">
      <Input v-model="userItem.email" placeholder="输入邮箱..."></Input>
    </FormItem>
      <FormItem label="手机：">
      <Input v-model="userItem.phone" placeholder="输入手机号..."></Input>
    </FormItem>
    <FormItem label="角色：" prop="role">
      <Select v-model="userItem.role"  style="width:260px" >
        <Option v-for="(item,index) in roleList" :value="item" :key="index">{{ item }}</Option>
      </Select>
    </FormItem>
    <FormItem>
      <Button type="primary" @click="save">保存</Button>
    </FormItem>
  </Form>
</template>
<script>
export default {
  props:['userItem'],
  data() {
    const validatePassCheck = (rule, value, callback) => {
      if (value === "") {
        callback(new Error("请再次输入密码"));
      } else if (value !== this.userItem.password) {
        callback(new Error("两次密码输入不一致"));
      } else {
        callback();
      }
    };
    const validateUser = (rule, value, callback) => {
      if (value === "") {
        return callback(new Error("请输入用户名"));
      } else {
        this.$api.user.getUserByUserName(value).then(res=>{
          if(res.success)
          {
             callback(new Error("用户名已存在"));
          }
          else{
             callback();
          }
        })
      }
    };
    return {
      roleList: [],
      selectRole: [],
      userValidate: {
        userName: [
          { type: "string", required: true, message: "用户名不能为空" },
          { validator: validateUser, required: true, trigger: "blur" }
        ],
         realName: [
          { type: "string", required: true, message: "姓名不能为空" }
        ],
        password: [{ type: "string", required: true, message: "密码不能为空" }],
        passwordConfirm: [
          { type: "string", required: true, message: "请确认密码" },
          { validator: validatePassCheck, required: true, trigger: "blur" }
        ],
        email: [{ type: "email", required: false, message: "邮箱格式不正确" }],
        role:[{ required: true,message: "请选择角色",trigger:"change" }]
      }
    };
  },
  watch: {
    // selectRole(newValue,oldValue){
    //   this.userItem.role=JSON.stringify(newValue);
    // }
  },
  methods: {
    save() {
      let _this = this;
      this.$refs["formValidate"].validate((valid) => {
        if (valid) {
          if(_this.userItem.id){
         
            _this.$api.user.updateUser(_this.userItem).then(res=>{
              if(res.success){
                  _this.$Message.success({
                  content: res.data.message
                });
                _this.$emit('on-Saved');
              }
              else{
                _this.$Message.error(res.data.message);
              }
            })
          }else{
               console.log(_this.userItem);
             _this.$api.user.addUser(_this.userItem).then(res=>{
              if(res.success){
                  _this.$Message.success({
                  content: res.data.message
                });
                _this.$emit('on-Saved');
              }
              else{
                _this.$Message.error(res.data.message);
              }
            })
          }
         
        }
      });
    },
  
  },
  mounted() {
    let _this = this;
    this.$api.user.getRoleList().then(res => {
       
        _this.roleList = res.data;
    });
  }
};
</script>
