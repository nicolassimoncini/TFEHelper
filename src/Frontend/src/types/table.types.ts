export interface DataType {
  id: string | number,
  key: string | number;
  title: string;
  authors: string;
  abstract: string;
  year: number | null;
  source: string;
  keywords: string;
  doi: string;
  isbn: string;
  issn: string;
  pages: string;
  url?: string;
}