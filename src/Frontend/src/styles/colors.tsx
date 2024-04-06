// colors.js

export interface IColors  {
  background: string;
  primary: string; 
  secondary: string, 
  accent:string;  
  success: string;
  warning:string; 
  danger:string;
    selected: string;
    hover: string;
}

const colors: IColors = {
  background: '#F5F5F5', // Slightly off-white background
  primary: '#6e6e6e', // Dark gray for primary text
  secondary: '#666666', // Lighter gray for secondary text
  accent: '#007bff', // Accent color (blue in this example)
  success: '#28a745', // Success color (green in this example)
  warning: '#ffc107', // Warning color (yellow in this example)
  danger: '#dc3545', // Danger color (red in this example)
selected: '#e0e0e0cb', // Selected color (light gray in this example)
  hover: '#dcdcdc', // Hover color (slightly lighter gray in this example)


};

export default colors;
