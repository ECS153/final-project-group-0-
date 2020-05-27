<template>
  <div id="home">
    <transition name="slide-fade" mode="out-in">
      <refresh-button
        v-if="requestSwap == null"
        @click="refresh"
      ></refresh-button>
      <swap-form
        v-else
        :swapProps="requestSwap"
        @submit="requestSwap = null"
      ></swap-form>
    </transition>
  </div>
</template>

<script>

import RefreshButton from "@/components/RefreshButton";
import SwapForm from "@/components/SwapForm";

export default {
  name: "Home",
  components: {
    SwapForm,
    RefreshButton
  },
  data() {
    return {
      requestSwap: null,
      
        
    };
  },
  methods: {
    refresh: function() {
      this.$http
        .request({
          url: "http://192.168.1.5:5000/pi/",
          method: "get",
          headers: {
            "Authorization": "BEARER " + this.$loginToken
          }
        })
        .then(resp => {
          setTimeout(() => {
            if (resp.status == 200) {
              this.requestSwap = resp.data;
            }
          }, 700);
        });
    }
  }
};
</script>

<style>
@import "../assets/css/Home.css";
@import "../assets/css/reset.css";
</style>
