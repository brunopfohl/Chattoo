/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL mutation operation: CreateCommunicationChannel
// ====================================================

export interface CreateCommunicationChannel_communicationChannels {
  __typename: "CommunicationChannelMutation";
  create: string | null;
}

export interface CreateCommunicationChannel {
  communicationChannels: CreateCommunicationChannel_communicationChannels | null;
}

export interface CreateCommunicationChannelVariables {
  name: string;
  desc: string;
}

/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: GetChannelsForUser
// ====================================================

export interface GetChannelsForUser_communicationChannels_getForUser_data {
  __typename: "CommunicationChannel";
  id: string;
  name: string;
  description: string;
  createdAt: any;
  modifiedAt: any | null;
}

export interface GetChannelsForUser_communicationChannels_getForUser {
  __typename: "PaginationListCommunicationChannelType";
  data: (GetChannelsForUser_communicationChannels_getForUser_data | null)[] | null;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  pageIndex: number;
  totalCount: number;
  totalPages: number;
}

export interface GetChannelsForUser_communicationChannels {
  __typename: "CommunicationChannelQuery";
  getForUser: GetChannelsForUser_communicationChannels_getForUser | null;
}

export interface GetChannelsForUser {
  communicationChannels: GetChannelsForUser_communicationChannels | null;
}

export interface GetChannelsForUserVariables {
  userId: string;
  pageNumber: number;
  pageSize: number;
}

/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL mutation operation: CreateMessage
// ====================================================

export interface CreateMessage_communicationChannelMessages {
  __typename: "CommunicationChannelMessageMutation";
  create: string | null;
}

export interface CreateMessage {
  communicationChannelMessages: CreateMessage_communicationChannelMessages | null;
}

export interface CreateMessageVariables {
  userId: string;
  channelId: string;
  content: string;
}

/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: GetMessagesForChannel
// ====================================================

export interface GetMessagesForChannel_communicationChannelMessages_getForChannel_data {
  __typename: "CommunicationChannelMessageType";
  id: string;
  content: string;
  type: asdf | null;
  userId: string;
  channelId: string;
  createdAt: any;
  modifiedAt: any | null;
}

export interface GetMessagesForChannel_communicationChannelMessages_getForChannel {
  __typename: "PaginationListCommunicationChannelMessageType";
  data: (GetMessagesForChannel_communicationChannelMessages_getForChannel_data | null)[] | null;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  pageIndex: number;
  totalCount: number;
  totalPages: number;
}

export interface GetMessagesForChannel_communicationChannelMessages {
  __typename: "CommunicationChannelMessageQuery";
  getForChannel: GetMessagesForChannel_communicationChannelMessages_getForChannel | null;
}

export interface GetMessagesForChannel {
  communicationChannelMessages: GetMessagesForChannel_communicationChannelMessages | null;
}

export interface GetMessagesForChannelVariables {
  channelId: string;
  pageNumber: number;
  pageSize: number;
}

/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL subscription operation: MessageAddedToChannelSubscription
// ====================================================

export interface MessageAddedToChannelSubscription_communicationChannelMessageAddedToChannel {
  __typename: "CommunicationChannelMessageType";
  id: string;
  createdAt: any;
  modifiedAt: any | null;
  content: string;
  channelId: string;
  userId: string;
  type: asdf | null;
}

export interface MessageAddedToChannelSubscription {
  communicationChannelMessageAddedToChannel: MessageAddedToChannelSubscription_communicationChannelMessageAddedToChannel | null;
}

export interface MessageAddedToChannelSubscriptionVariables {
  channelId: string;
}

/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: GetUsers
// ====================================================

export interface GetUsers_users_get_data {
  __typename: "UserType";
  id: string;
  userName: string;
}

export interface GetUsers_users_get {
  __typename: "PaginationListUserType";
  data: (GetUsers_users_get_data | null)[] | null;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  pageIndex: number;
  totalCount: number;
  totalPages: number;
}

export interface GetUsers_users {
  __typename: "UserQuery";
  get: GetUsers_users_get | null;
}

export interface GetUsers {
  users: GetUsers_users | null;
}

export interface GetUsersVariables {
  searchTerm: string;
}

/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: GetUsersForChannel
// ====================================================

export interface GetUsersForChannel_users_getForCommunicationChannel_data {
  __typename: "UserType";
  id: string;
  userName: string;
  createdAt: any;
  modifiedAt: any | null;
}

export interface GetUsersForChannel_users_getForCommunicationChannel {
  __typename: "PaginationListUserType";
  data: (GetUsersForChannel_users_getForCommunicationChannel_data | null)[] | null;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  pageIndex: number;
  totalCount: number;
  totalPages: number;
}

export interface GetUsersForChannel_users {
  __typename: "UserQuery";
  getForCommunicationChannel: GetUsersForChannel_users_getForCommunicationChannel | null;
}

export interface GetUsersForChannel {
  users: GetUsersForChannel_users | null;
}

export interface GetUsersForChannelVariables {
  channelId: string;
}

/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

//==============================================================
// START Enums and Input Objects
//==============================================================

export enum asdf {
  ANNOUNCEMENT = "ANNOUNCEMENT",
  NORMAL = "NORMAL",
}

//==============================================================
// END Enums and Input Objects
//==============================================================
