import { AxiosError } from 'axios';
import Swal from 'sweetalert2';

export const SwalError = (err: AxiosError) => {
  return Swal.fire({
    title: 'Sorry, something went wrong!',
    text: err.message,
    icon: 'error',
    confirmButtonColor: '#3085d6',
    customClass: {
      container: 'my-swal',
    },
  });
};

export const deactivateAlert = (title: string) => {
  return Swal.fire({
    title: title,
    icon: 'warning',
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    showCancelButton: true,
    confirmButtonText: 'Yes',
  });
};
export const successAlert = (title: string) => {
  return Swal.fire({
    title: title,
    icon: 'success',
    confirmButtonColor: '#3085d6',
    customClass: {
      container: 'my-swal',
    },
  });
};
