import axios from '@/libs/api.request'
//与用户相关的api

export default {
    //登录
    login({ userName, password }) {
        const data = {
            userName,
            password
        }
        return axios.request({
            url: 'api/token',
            data,
            method: 'post',
        })
    },
    //获取用户信息
    getUserInfo() {
        return axios.request({
            url: 'api/v1/User/GetCurrentUser',
            method: 'get',
        })
    },
    getUserByUserName(username) {
        return axios.request({
            url: 'api/v1/User/GetByUserName/' + username,
            method: 'get',
        })
    },
    getUserByUserId(id) {
        return axios.request({
            url: 'api/v1/User/GetByUserId/' + id,
            method: 'get',
        })
    },


    getUserList({ userName, page, size }) {
        return axios.request({
            url: 'api/v1/User/List',
            method: 'get',
            params: { userName, page, size }
        })
    },
    getRoleList() {
        return axios.request({
            url: 'api/v1/User/GetRoleList',
            method: 'get',
        })
    },
    addUser(user) {
        return axios.request({
            url: 'api/v1/User/Add',
            data: user,
            method: 'post'
        })
    },
    updateUser(user) {
        return axios.request({
            url: 'api/v1/User/Update',
            data: user,
            method: 'post'
        })
    }
}


