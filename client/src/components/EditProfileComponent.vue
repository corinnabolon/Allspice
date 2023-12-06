<template>
  <div>
    <form @submit.prevent="editProfile">
      <div class="mb-3">
        <label for="name" class="form-label">
          <p class="serif-font fs-5 mb-0">Username</p>
        </label>
        <input v-model="editable.name" type="text" class="form-control" id="name" placeholder="Your username..." required
          maxLength="255">
      </div>
      <div class="mb-3">
        <label for="picture" class="form-label">
          <p class="serif-font fs-5 mb-0">Avatar</p>
        </label>
        <input v-model="editable.picture" type="url" class="form-control" id="picture"
          placeholder="Image URL of your avatar..." required maxLength="255">
      </div>
      <div class="d-flex justify-content-end mt-3">
        <button type="button" class="btn btn-theme-orange me-3" data-bs-dismiss="modal"
          aria-label="Cancel">Cancel</button>
        <button type="submit" class="btn btn-theme-green">Submit</button>
      </div>
    </form>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from "../utils/Pop.js";
import { Modal } from "bootstrap";
import { accountService } from "../services/AccountService.js";
import { logger } from "../utils/Logger.js";

export default {
  setup() {
    let editable = ref({})

    onMounted(() => {
      let editProfileComponentElem = document.getElementById('editProfileComponent')
      editProfileComponentElem.addEventListener('show.bs.modal', function (event) {
        logger.log("Triggered")
        editable.value = JSON.parse(JSON.stringify(AppState.account));
      })
      editProfileComponentElem.addEventListener('hidden.bs.modal', function (event) {
        logger.log("Triggered")
        editable.value = JSON.parse(JSON.stringify(AppState.account));
      })
      editable.value = JSON.parse(JSON.stringify(AppState.account));
    })

    //TODO: onMounted reset editable to ref AppState.account

    return {
      editable,

      async editProfile() {
        try {
          let editedData = editable.value
          await accountService.editAccount(editedData)
          Pop.success("Profile edited!")
          Modal.getOrCreateInstance("#editProfileComponent").hide()
        } catch (error) {
          Pop.error(error)
        }
      }

    }
  }
};
</script>


<style lang="scss" scoped></style>