<template>
  <div>
    <form @submit.prevent="createRecipe">
      <div class="mb-3">
        <label for="title" class="form-label">
          <p class="serif-font fs-5 mb-0">Title</p>
        </label>
        <input v-model="editable.title" type="text" class="form-control" id="title" placeholder="Title..." required
          maxLength="255">
      </div>
      <div class="mb-3">
        <label for="category" class="form-label">
          <p class="serif-font fs-5 mb-0">Category</p>
        </label>
        <select v-model="editable.category" class="form-select" aria-label="Default select example" required>
          <option v-for="category in categories" :key="categories" :value="category">{{ category }}</option>
        </select>
      </div>
      <div>
        <label for="img" class="form-label">
          <p class="serif-font fs-5 mb-0">Image URL</p>
        </label>
        <input v-model="editable.img" type="text" class="form-control" id="title" placeholder="Image URL..." required
          maxLength="1000">
      </div>
      <div class="d-flex justify-content-end mt-3">
        <button type="button" class="btn btn-theme-orange me-3" data-bs-dismiss="modal" aria-label="Close">Close</button>
        <button type="submit" class="btn btn-theme-green">Submit</button>
      </div>
    </form>


  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
  setup() {
    return {}
  }
};
</script>


<style lang="scss" scoped></style>