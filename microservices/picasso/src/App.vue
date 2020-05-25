<template>
  <div id="app">
    <transition name="slide-fade" mode="out-in">
      <RefreshButton v-if="requestSwap==null" @click="refresh"></RefreshButton>
      <CredentialForm v-else :token="loginToken" :swapProps="requestSwap" @submit="requestSwap=null"></CredentialForm>
    </transition>
  </div>
</template>

<script>
import axios from 'axios'
import RefreshButton from './components/RefreshButton'
import CredentialForm from './components/CredentialForm'

export default {
  name: 'App',
  components: {
    CredentialForm,
    RefreshButton,
  },
  data () {
    return {
      requestSwap: null,
      selectedCred: "",
      loginToken: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiVXNlciIsIm5iZiI6MTU5MDM3ODg1MiwiZXhwIjoxNTkwNDY1MjUyLCJpYXQiOjE1OTAzNzg4NTJ9.ztoxoxV_0QqRkjiNZuddA32z6tFAFcWnKHIKqMaw--A',
      availableCreds: null,
    }
  }, 
  computed: {
    credHints: function() {
      if (this.availableCreds != null) 
        return this.availableCreds.map(h => h.hint && h.id)
      return null
    }
  },
  methods: {
    refresh: function() {
      axios.request({
         url: 'http://192.168.1.5:5000/pi/',
        method: 'get',
        headers : {
          'Authorization': 'BEARER ' + this.loginToken
        }
      })
      .then(resp => {
          if (resp.status == 200) {
            this.requestSwap = resp.data
          }     
      })
    }
  }
}
</script>

<style>
  @import './assets/app.css';
  @import './assets/reset.css';
</style>
