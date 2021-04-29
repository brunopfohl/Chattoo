export interface PaginatedList<T> {
    data: (T | null)[],
    hasNextPage: boolean;
    hasPreviousPage: boolean;
    pageIndex: number;
    totalCount: number;
    totalPages: number;
}