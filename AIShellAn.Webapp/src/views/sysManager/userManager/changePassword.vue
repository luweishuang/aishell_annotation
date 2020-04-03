<template>
  <Card>
    <p slot="title">
      <Icon type="ios-film-outline"></Icon>修改密码
    </p>
    <Form :model="userItem" ref="formValidate" :label-width="100" :rules="userValidate">
      <FormItem label="原密码：" prop="oldPassword">
        <Input
          type="password"
          autocomplete="off"
          v-model="userItem.oldPassword"
          placeholder="输入原密码..."
        />
      </FormItem>
      <FormItem label="新密码：" prop="password">
        <Input
          type="password"
          autocomplete="off"
          v-model="userItem.password"
          placeholder="输入新密码..."
        />
      </FormItem>
      <FormItem label="确认新密码：" prop="passwordConfirm">
        <Input
          type="password"
          autocomplete="off"
          v-model="userItem.passwordConfirm"
          placeholder="确认密码..."
        />
      </FormItem>
      <FormItem>
        <Button type="primary" @click="save">保存</Button>
      </FormItem>
    </Form>
  </Card>
</template>
<script>
export default {
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
    return {
      userItem: {
        oldPassword: "",
        password: "",
        passwordConfirm: ""
      },
      userValidate: {
        oldPassword: [
          { type: "string", required: true, message: "原密码不能为空" }
        ],
        password: [
          { type: "string", required: true, message: "新密码不能为空" }
        ],
        passwordConfirm: [
          { type: "string", required: true, message: "请确认新密码" },
          { validator: validatePassCheck, required: true, trigger: "blur" }
        ]
      }
    };
  },
  methods: {
    save() {
      let _this = this;
      _this.$refs.formValidate.validate(valid => {
        if (valid) {
            let data={
               oldPassword: _this.userItem.oldPassword,
               password: _this.userItem.password
            }
          changePassword(data)
            .then(res => {
              _this.$Message.success(res.data.message);
            })
            .catch(err => {
              _this.$Message.success(err.response.data.message);
            });
        }
      });
    }
  }
};
</script>
