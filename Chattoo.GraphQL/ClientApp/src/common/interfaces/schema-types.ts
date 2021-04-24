/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: GetGroup
// ====================================================

export interface GetGroup_groups_get {
  __typename: "GroupType";
  id: string;
  createdAt: any;
  modifiedAt: any | null;
  name: string;
}

export interface GetGroup_groups {
  __typename: "GroupQuery";
  get: GetGroup_groups_get | null;
}

export interface GetGroup {
  groups: GetGroup_groups | null;
}

export interface GetGroupVariables {
  id?: string | null;
}

/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: GetGroupsForUser
// ====================================================

export interface GetGroupsForUser_groups_getForUser_data {
  __typename: "GroupType";
  id: string;
  name: string;
  createdAt: any;
  modifiedAt: any | null;
}

export interface GetGroupsForUser_groups_getForUser {
  __typename: "PaginationListGroupType";
  data: (GetGroupsForUser_groups_getForUser_data | null)[] | null;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  pageIndex: number;
  totalCount: number;
  totalPages: number;
}

export interface GetGroupsForUser_groups {
  __typename: "GroupQuery";
  getForUser: GetGroupsForUser_groups_getForUser | null;
}

export interface GetGroupsForUser {
  groups: GetGroupsForUser_groups | null;
}

export interface GetGroupsForUserVariables {
  userId?: string | null;
}

/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: GetUserAlias
// ====================================================

export interface GetUserAlias_userAliases_get {
  __typename: "UserAliasType";
  id: string;
  userId: string;
  alias: string;
  createdAt: any;
  modifiedAt: any | null;
}

export interface GetUserAlias_userAliases {
  __typename: "UserAliasQuery";
  get: GetUserAlias_userAliases_get | null;
}

export interface GetUserAlias {
  userAliases: GetUserAlias_userAliases | null;
}

export interface GetUserAliasVariables {
  id?: string | null;
}

/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: GetAliasesForUser
// ====================================================

export interface GetAliasesForUser_userAliases_getForUser_data {
  __typename: "UserAliasType";
  id: string;
  userId: string;
  alias: string;
  createdAt: any;
  modifiedAt: any | null;
}

export interface GetAliasesForUser_userAliases_getForUser {
  __typename: "PaginationListUserAliasType";
  data: (GetAliasesForUser_userAliases_getForUser_data | null)[] | null;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  pageIndex: number;
  totalCount: number;
  totalPages: number;
}

export interface GetAliasesForUser_userAliases {
  __typename: "UserAliasQuery";
  getForUser: GetAliasesForUser_userAliases_getForUser | null;
}

export interface GetAliasesForUser {
  userAliases: GetAliasesForUser_userAliases | null;
}

export interface GetAliasesForUserVariables {
  userId?: string | null;
}

/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: GetUsersForGroup
// ====================================================

export interface GetUsersForGroup_users_getForGroup_data {
  __typename: "UserType";
  createdAt: any;
  id: string;
  modifiedAt: any | null;
}

export interface GetUsersForGroup_users_getForGroup {
  __typename: "PaginationListUserType";
  data: (GetUsersForGroup_users_getForGroup_data | null)[] | null;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  pageIndex: number;
  totalCount: number;
  totalPages: number;
}

export interface GetUsersForGroup_users {
  __typename: "UserQuery";
  getForGroup: GetUsersForGroup_users_getForGroup | null;
}

export interface GetUsersForGroup {
  users: GetUsersForGroup_users | null;
}

export interface GetUsersForGroupVariables {
  groupId?: string | null;
}

/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: GetUsersForChannel
// ====================================================

export interface GetUsersForChannel_users_getForGroup_data {
  __typename: "UserType";
  id: string;
  createdAt: any;
  modifiedAt: any | null;
}

export interface GetUsersForChannel_users_getForGroup {
  __typename: "PaginationListUserType";
  data: (GetUsersForChannel_users_getForGroup_data | null)[] | null;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  pageIndex: number;
  totalCount: number;
  totalPages: number;
}

export interface GetUsersForChannel_users {
  __typename: "UserQuery";
  getForGroup: GetUsersForChannel_users_getForGroup | null;
}

export interface GetUsersForChannel {
  users: GetUsersForChannel_users | null;
}

export interface GetUsersForChannelVariables {
  channelId?: string | null;
}

/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

//==============================================================
// START Enums and Input Objects
//==============================================================

//==============================================================
// END Enums and Input Objects
//==============================================================
