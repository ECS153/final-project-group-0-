<template>
  <div id="home">
    
      
    <transition name="slide-fade" mode="out-in">
      <refresh-button v-if="requestSwap == null" @click="refresh"></refresh-button>
      <swap-form v-else :swapProps="requestSwap" @submit="requestSwap = null"></swap-form>
    </transition>
  </div>
</template>

<script>
/* eslint-disable */
import "@/assets/css/views/home.css";
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
      requestSwap: null
    };
  },
  methods: {
    refresh: function() {
      this.$http
        .request({
          url: "http://localhost:8000/swap/",
          method: "get",
          headers: this.$apiHeaders
        })
        .then(resp => {
          setTimeout(() => {
            if (resp.status == 200) {
              this.requestSwap = resp.data;
            }
          }, 700);
        })
        .catch ((err) => {
          if (err.response.status == 401) {
            this.$toast.error("Unauthorized");
          } else {
            this.$toast.error(err.response.data.title);
          }
      });
    }
  }
};
</script>
