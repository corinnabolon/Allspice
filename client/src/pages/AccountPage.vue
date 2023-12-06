<template>
  <div class="about text-center mt-5">
    <p class="fs-1 serif-font text-theme-green">Welcome {{ account.name }}</p>
    <img class="rounded" :src="account.picture" alt="" />
    <p class="fs-5 serif-font text-theme-green mt-3">{{ account.email }}</p>
  </div>
  <div class="d-flex justify-content-center mt-3">
    <button class="btn btn-theme-green" data-bs-toggle="modal" data-bs-target="#editProfileComponent">Edit Your
      Profile</button>
  </div>

  <ModalComponent :modalId="'editProfileComponent'" :modalSize="'modal-md'">
    <template #modalTitle>
      <p class="serif-font fs-5">Your Profile Details</p>
    </template>
    <template #modalBody>
      <EditProfileComponent />
    </template>
  </ModalComponent>
</template>

<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
import ModalComponent from '../components/ModalComponent.vue';
import { Modal } from "bootstrap";
import EditProfileComponent from '../components/EditProfileComponent.vue';

export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  },
  components: { ModalComponent, EditProfileComponent }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
