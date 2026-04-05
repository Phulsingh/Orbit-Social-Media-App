import axiosInstance from "../config/axiosConfig";

export const apiService = {
  get: async (url: string) => {
    const res = await axiosInstance.get(url);
    return res.data;
  },

  post: async (url: string, data: any) => {
    const res = await axiosInstance.post(url, data);
    return res.data;
  },

  put: async (url: string, data: any) => {
    const res = await axiosInstance.put(url, data);
    return res.data;
  },

  delete: async (url: string) => {
    const res = await axiosInstance.delete(url);
    return res.data;
  },
};