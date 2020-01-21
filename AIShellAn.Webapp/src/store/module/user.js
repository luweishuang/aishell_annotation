import { setToken, getToken } from '@/libs/util'
import { setTagNavListInLocalstorage} from '@/libs/util'
import userApi from '@/api/user'
export default {
  state: {
    userName: '',
    name:'',
    userId: '',
    avatorImgPath:'',
    token: getToken(),
    role:[],
  },
  mutations: {
    setAvator (state, avatorPath) {
      state.avatorImgPath = avatorPath;
    },
    setUserId (state, id) {
      state.userId = id;
    },
    setUserName (state, name) {
      state.userName = name;
    },
    setName(state,name){
      state.name=name;
    },
    setToken (state, token) {
      state.token = token;
      setToken(token);
    },
    setRole(state,role){
      state.role = role;
    }
  },
  actions: {
    // 登录
    handleLogin ({ state,commit }, {userName, password}) {
      userName = userName.trim()
      return new Promise((resolve, reject) => {
        userApi.login({userName, password}).then(res => {
          
          if(res.Status){
            commit('setToken', res.access_token);
            resolve(res);
          }
          else{
            reject(res);
          }
        })
      })
    },
    // 退出登录
    handleLogOut ({ state, commit }) {
      return new Promise((resolve, reject) => {
        //退出登录，请求服务器，清除session
        // logout(state.token).then(() => {
        //   commit('setToken', '')
        //   commit('setAccess', [])
        //   resolve()
        // }).catch(err => {
          
        //   reject(err)
        // })
        // 如果你的退出登录无需请求接口，则可以直接使用下面三行代码而无需使用logout调用接口
        commit('setToken', '');
        setTagNavListInLocalstorage([]);
        resolve();
      })
    },
    // 获取用户相关信息
    getUserInfo ({ state, commit }) {
      return new Promise((resolve, reject) => {
        try {
          userApi.getUserInfo().then(res => {
            if(res.success){
              const data = res.data;
              commit('setUserName', data.userName)
              commit('setName', data.realName)
              commit('setUserId', data.id)
              commit('setRole',data.role.split(','));
              resolve(data);
            }else{
              reject(res)
            }
          }).catch(err => {
            reject(err)
          })
        } catch (error) {
          reject(error)
        }
      })
    },
   
  }
}
