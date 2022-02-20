import { gql } from '@apollo/client';
import * as Apollo from '@apollo/client';
export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
const defaultOptions =  {}
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  /** The `Date` scalar type represents a year, month and day in accordance with the [ISO-8601](https://en.wikipedia.org/wiki/ISO_8601) standard. */
  Date: any;
  /** The `DateTime` scalar type represents a date and time. `DateTime` expects timestamps to be formatted in accordance with the [ISO-8601](https://en.wikipedia.org/wiki/ISO_8601) standard. */
  DateTime: any;
};

export type CalendarEvent = {
  __typename?: 'CalendarEvent';
  authorId: Scalars['String'];
  communicationChannelId?: Maybe<Scalars['String']>;
  createdAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy: Scalars['String'];
  description: Scalars['String'];
  endsAt?: Maybe<Scalars['DateTime']>;
  groupId?: Maybe<Scalars['String']>;
  id: Scalars['String'];
  maximalParticipantsCount?: Maybe<Scalars['Int']>;
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy: Scalars['String'];
  name: Scalars['String'];
  startsAt: Scalars['DateTime'];
  type?: Maybe<CalendarEventType>;
};

export type CalendarEventMutation = {
  __typename?: 'CalendarEventMutation';
  create?: Maybe<CalendarEvent>;
  delete?: Maybe<Scalars['Boolean']>;
  update?: Maybe<Scalars['Boolean']>;
};


export type CalendarEventMutationCreateArgs = {
  channelId?: InputMaybe<Scalars['String']>;
  desc: Scalars['String'];
  endsAt?: InputMaybe<Scalars['Date']>;
  eventTypeId?: InputMaybe<Scalars['String']>;
  groupId?: InputMaybe<Scalars['String']>;
  maximalParticipantsCount?: InputMaybe<Scalars['Int']>;
  name: Scalars['String'];
  startsAt: Scalars['Date'];
};


export type CalendarEventMutationDeleteArgs = {
  id: Scalars['String'];
};


export type CalendarEventMutationUpdateArgs = {
  desc: Scalars['String'];
  endsAt: Scalars['Date'];
  id: Scalars['String'];
  maximalParticipantsCount?: InputMaybe<Scalars['Int']>;
  name: Scalars['String'];
  startsAt?: InputMaybe<Scalars['Date']>;
};

export type CalendarEventType = {
  __typename?: 'CalendarEventType';
  createdAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy: Scalars['String'];
  id: Scalars['String'];
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy: Scalars['String'];
  name: Scalars['String'];
};

export type CommunicationChannel = {
  __typename?: 'CommunicationChannel';
  createdAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy: Scalars['String'];
  description: Scalars['String'];
  id: Scalars['String'];
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy: Scalars['String'];
  name: Scalars['String'];
};

export type CommunicationChannelCalendarEventQuery = {
  __typename?: 'CommunicationChannelCalendarEventQuery';
  get?: Maybe<CalendarEvent>;
  getForCommunicationChannel?: Maybe<PaginationListCalendarEventGraphType>;
  getForGroup?: Maybe<PaginationListCalendarEventGraphType>;
};


export type CommunicationChannelCalendarEventQueryGetArgs = {
  id: Scalars['ID'];
};


export type CommunicationChannelCalendarEventQueryGetForCommunicationChannelArgs = {
  channelId: Scalars['String'];
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
};


export type CommunicationChannelCalendarEventQueryGetForGroupArgs = {
  groupId: Scalars['String'];
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
};

export type CommunicationChannelMessage = {
  __typename?: 'CommunicationChannelMessage';
  channelId: Scalars['String'];
  content: Scalars['String'];
  createdAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy: Scalars['String'];
  id: Scalars['String'];
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy: Scalars['String'];
  type?: Maybe<CommunicationChannelMessageType>;
  userId: Scalars['String'];
  userName: Scalars['String'];
};

export type CommunicationChannelMessageAttachmentMutation = {
  __typename?: 'CommunicationChannelMessageAttachmentMutation';
  create?: Maybe<Scalars['String']>;
  delete?: Maybe<Scalars['Boolean']>;
  update?: Maybe<Scalars['Boolean']>;
};


export type CommunicationChannelMessageAttachmentMutationCreateArgs = {
  content: Scalars['String'];
  messageId: Scalars['String'];
  name: Scalars['String'];
  permission: Scalars['Int'];
};


export type CommunicationChannelMessageAttachmentMutationDeleteArgs = {
  channelId: Scalars['String'];
  id: Scalars['String'];
  messageId: Scalars['String'];
};


export type CommunicationChannelMessageAttachmentMutationUpdateArgs = {
  channelId: Scalars['String'];
  id: Scalars['String'];
  messageId: Scalars['String'];
  name: Scalars['String'];
};

export type CommunicationChannelMessageMutation = {
  __typename?: 'CommunicationChannelMessageMutation';
  create?: Maybe<CommunicationChannelMessage>;
  delete?: Maybe<Scalars['Boolean']>;
  update?: Maybe<Scalars['Boolean']>;
};


export type CommunicationChannelMessageMutationCreateArgs = {
  channelId: Scalars['String'];
  content: Scalars['String'];
  type: Scalars['Int'];
};


export type CommunicationChannelMessageMutationDeleteArgs = {
  channelId: Scalars['String'];
  id: Scalars['String'];
};


export type CommunicationChannelMessageMutationUpdateArgs = {
  channelId: Scalars['String'];
  content: Scalars['String'];
  id: Scalars['String'];
};

export type CommunicationChannelMessageQuery = {
  __typename?: 'CommunicationChannelMessageQuery';
  get?: Maybe<CommunicationChannelMessage>;
  getForChannel?: Maybe<PaginationListCommunicationChannelMessageGraphType>;
};


export type CommunicationChannelMessageQueryGetArgs = {
  channelId: Scalars['ID'];
  messageId: Scalars['ID'];
};


export type CommunicationChannelMessageQueryGetForChannelArgs = {
  channelId: Scalars['ID'];
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
};

export enum CommunicationChannelMessageType {
  Announcement = 'ANNOUNCEMENT',
  Normal = 'NORMAL'
}

export type CommunicationChannelMutation = {
  __typename?: 'CommunicationChannelMutation';
  addUser?: Maybe<Scalars['Boolean']>;
  create?: Maybe<CommunicationChannel>;
  delete?: Maybe<Scalars['Boolean']>;
  removeUser?: Maybe<Scalars['Boolean']>;
  update?: Maybe<Scalars['Boolean']>;
};


export type CommunicationChannelMutationAddUserArgs = {
  channelId: Scalars['String'];
  userId: Scalars['String'];
};


export type CommunicationChannelMutationCreateArgs = {
  desc: Scalars['String'];
  name: Scalars['String'];
};


export type CommunicationChannelMutationDeleteArgs = {
  id: Scalars['String'];
};


export type CommunicationChannelMutationRemoveUserArgs = {
  channelId: Scalars['String'];
  userId: Scalars['String'];
};


export type CommunicationChannelMutationUpdateArgs = {
  desc: Scalars['String'];
  id: Scalars['String'];
  name: Scalars['String'];
};

export type CommunicationChannelQuery = {
  __typename?: 'CommunicationChannelQuery';
  get?: Maybe<CommunicationChannel>;
  getForUser?: Maybe<PaginationListCommunicationChannelGraphType>;
};


export type CommunicationChannelQueryGetArgs = {
  id: Scalars['ID'];
};


export type CommunicationChannelQueryGetForUserArgs = {
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
  userId: Scalars['String'];
};

export type CommunicationChannelRole = {
  __typename?: 'CommunicationChannelRole';
  channelId: Scalars['String'];
  createdAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy: Scalars['String'];
  id: Scalars['String'];
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy: Scalars['String'];
  name: Scalars['String'];
};

export type CommunicationChannelRoleMutation = {
  __typename?: 'CommunicationChannelRoleMutation';
  create?: Maybe<Scalars['String']>;
  delete?: Maybe<Scalars['Boolean']>;
  update?: Maybe<Scalars['Boolean']>;
};


export type CommunicationChannelRoleMutationCreateArgs = {
  channelId: Scalars['String'];
  name: Scalars['String'];
  permission: Scalars['Int'];
};


export type CommunicationChannelRoleMutationDeleteArgs = {
  channelId: Scalars['String'];
  id: Scalars['String'];
};


export type CommunicationChannelRoleMutationUpdateArgs = {
  channelId: Scalars['String'];
  content: Scalars['String'];
  id: Scalars['String'];
};

export type CommunicationChannelRoleQuery = {
  __typename?: 'CommunicationChannelRoleQuery';
  get?: Maybe<CommunicationChannelRole>;
  getForChannel?: Maybe<PaginationListCommunicationChannelRoleGraphType>;
  getForUserInChannel?: Maybe<PaginationListCommunicationChannelRoleGraphType>;
};


export type CommunicationChannelRoleQueryGetArgs = {
  channelId: Scalars['ID'];
  id: Scalars['ID'];
};


export type CommunicationChannelRoleQueryGetForChannelArgs = {
  channelId: Scalars['ID'];
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
};


export type CommunicationChannelRoleQueryGetForUserInChannelArgs = {
  channelId: Scalars['ID'];
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
  userId: Scalars['ID'];
};

export type Group = {
  __typename?: 'Group';
  createdAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy: Scalars['String'];
  id: Scalars['String'];
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy: Scalars['String'];
  name: Scalars['String'];
};

export type GroupMutation = {
  __typename?: 'GroupMutation';
  addUser?: Maybe<Scalars['Boolean']>;
  create?: Maybe<Scalars['String']>;
  delete?: Maybe<Scalars['Boolean']>;
  removeUser?: Maybe<Scalars['Boolean']>;
  update?: Maybe<Scalars['Boolean']>;
};


export type GroupMutationAddUserArgs = {
  groupId: Scalars['String'];
  userId: Scalars['String'];
};


export type GroupMutationCreateArgs = {
  name: Scalars['String'];
};


export type GroupMutationDeleteArgs = {
  id: Scalars['String'];
};


export type GroupMutationRemoveUserArgs = {
  groupId: Scalars['String'];
  userId: Scalars['String'];
};


export type GroupMutationUpdateArgs = {
  id: Scalars['String'];
  name: Scalars['String'];
};

export type GroupQuery = {
  __typename?: 'GroupQuery';
  get?: Maybe<Group>;
  getForUser?: Maybe<PaginationListGroupGraphType>;
};


export type GroupQueryGetArgs = {
  id: Scalars['ID'];
};


export type GroupQueryGetForUserArgs = {
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
  userId: Scalars['ID'];
};

export type GroupRole = {
  __typename?: 'GroupRole';
  createdAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy: Scalars['String'];
  id: Scalars['String'];
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy: Scalars['String'];
  name: Scalars['String'];
};

export type GroupRoleMutation = {
  __typename?: 'GroupRoleMutation';
  create?: Maybe<Scalars['String']>;
  delete?: Maybe<Scalars['Boolean']>;
  update?: Maybe<Scalars['Boolean']>;
};


export type GroupRoleMutationCreateArgs = {
  groupId: Scalars['String'];
  name: Scalars['String'];
  permission: Scalars['Int'];
};


export type GroupRoleMutationDeleteArgs = {
  groupId: Scalars['String'];
  id: Scalars['String'];
};


export type GroupRoleMutationUpdateArgs = {
  groupId: Scalars['String'];
  id: Scalars['String'];
  name: Scalars['String'];
  permission: Scalars['Int'];
};

export type GroupRoleQuery = {
  __typename?: 'GroupRoleQuery';
  get?: Maybe<GroupRole>;
  getForUserInGroup?: Maybe<PaginationListGroupRoleGraphType>;
};


export type GroupRoleQueryGetArgs = {
  groupId: Scalars['ID'];
  roleId: Scalars['ID'];
};


export type GroupRoleQueryGetForUserInGroupArgs = {
  groupId: Scalars['ID'];
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
  userId: Scalars['ID'];
};

export type Mutation = {
  __typename?: 'Mutation';
  communicationChannelCalendarEvents?: Maybe<CalendarEventMutation>;
  communicationChannelMessageAttachments?: Maybe<CommunicationChannelMessageAttachmentMutation>;
  communicationChannelMessages?: Maybe<CommunicationChannelMessageMutation>;
  communicationChannelRoles?: Maybe<CommunicationChannelRoleMutation>;
  communicationChannels?: Maybe<CommunicationChannelMutation>;
  groupRoles?: Maybe<GroupRoleMutation>;
  groups?: Maybe<GroupMutation>;
  userAliases?: Maybe<UserAliasMutation>;
};

export type PaginationListCalendarEventGraphType = {
  __typename?: 'PaginationListCalendarEventGraphType';
  data?: Maybe<Array<Maybe<CalendarEvent>>>;
  hasNextPage: Scalars['Boolean'];
  hasPreviousPage: Scalars['Boolean'];
  pageIndex: Scalars['Int'];
  totalCount: Scalars['Int'];
  totalPages: Scalars['Int'];
};

export type PaginationListCommunicationChannelGraphType = {
  __typename?: 'PaginationListCommunicationChannelGraphType';
  data?: Maybe<Array<Maybe<CommunicationChannel>>>;
  hasNextPage: Scalars['Boolean'];
  hasPreviousPage: Scalars['Boolean'];
  pageIndex: Scalars['Int'];
  totalCount: Scalars['Int'];
  totalPages: Scalars['Int'];
};

export type PaginationListCommunicationChannelMessageGraphType = {
  __typename?: 'PaginationListCommunicationChannelMessageGraphType';
  data?: Maybe<Array<Maybe<CommunicationChannelMessage>>>;
  hasNextPage: Scalars['Boolean'];
  hasPreviousPage: Scalars['Boolean'];
  pageIndex: Scalars['Int'];
  totalCount: Scalars['Int'];
  totalPages: Scalars['Int'];
};

export type PaginationListCommunicationChannelRoleGraphType = {
  __typename?: 'PaginationListCommunicationChannelRoleGraphType';
  data?: Maybe<Array<Maybe<CommunicationChannelRole>>>;
  hasNextPage: Scalars['Boolean'];
  hasPreviousPage: Scalars['Boolean'];
  pageIndex: Scalars['Int'];
  totalCount: Scalars['Int'];
  totalPages: Scalars['Int'];
};

export type PaginationListGroupGraphType = {
  __typename?: 'PaginationListGroupGraphType';
  data?: Maybe<Array<Maybe<Group>>>;
  hasNextPage: Scalars['Boolean'];
  hasPreviousPage: Scalars['Boolean'];
  pageIndex: Scalars['Int'];
  totalCount: Scalars['Int'];
  totalPages: Scalars['Int'];
};

export type PaginationListGroupRoleGraphType = {
  __typename?: 'PaginationListGroupRoleGraphType';
  data?: Maybe<Array<Maybe<GroupRole>>>;
  hasNextPage: Scalars['Boolean'];
  hasPreviousPage: Scalars['Boolean'];
  pageIndex: Scalars['Int'];
  totalCount: Scalars['Int'];
  totalPages: Scalars['Int'];
};

export type PaginationListUserAliasGraphType = {
  __typename?: 'PaginationListUserAliasGraphType';
  data?: Maybe<Array<Maybe<UserAlias>>>;
  hasNextPage: Scalars['Boolean'];
  hasPreviousPage: Scalars['Boolean'];
  pageIndex: Scalars['Int'];
  totalCount: Scalars['Int'];
  totalPages: Scalars['Int'];
};

export type PaginationListUserGraphType = {
  __typename?: 'PaginationListUserGraphType';
  data?: Maybe<Array<Maybe<User>>>;
  hasNextPage: Scalars['Boolean'];
  hasPreviousPage: Scalars['Boolean'];
  pageIndex: Scalars['Int'];
  totalCount: Scalars['Int'];
  totalPages: Scalars['Int'];
};

export type Query = {
  __typename?: 'Query';
  communicationChannelCalendarEvents?: Maybe<CommunicationChannelCalendarEventQuery>;
  communicationChannelMessages?: Maybe<CommunicationChannelMessageQuery>;
  communicationChannelRoles?: Maybe<CommunicationChannelRoleQuery>;
  communicationChannels?: Maybe<CommunicationChannelQuery>;
  groupRoles?: Maybe<GroupRoleQuery>;
  groups?: Maybe<GroupQuery>;
  userAliases?: Maybe<UserAliasQuery>;
  users?: Maybe<UserQuery>;
};

export type Subscription = {
  __typename?: 'Subscription';
  communicationChannelAddedForUser?: Maybe<CommunicationChannel>;
  communicationChannelMessageAddedToChannel?: Maybe<CommunicationChannelMessage>;
};


export type SubscriptionCommunicationChannelAddedForUserArgs = {
  userId: Scalars['String'];
};


export type SubscriptionCommunicationChannelMessageAddedToChannelArgs = {
  channelId: Scalars['String'];
};

export type User = {
  __typename?: 'User';
  createdAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy: Scalars['String'];
  id: Scalars['String'];
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy: Scalars['String'];
  userName: Scalars['String'];
};

export type UserAlias = {
  __typename?: 'UserAlias';
  alias: Scalars['String'];
  createdAt: Scalars['DateTime'];
  createdBy: Scalars['String'];
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy: Scalars['String'];
  id: Scalars['String'];
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy: Scalars['String'];
  userId: Scalars['String'];
};

export type UserAliasMutation = {
  __typename?: 'UserAliasMutation';
  create?: Maybe<Scalars['String']>;
  delete?: Maybe<Scalars['Boolean']>;
  update?: Maybe<Scalars['Boolean']>;
};


export type UserAliasMutationCreateArgs = {
  alias: Scalars['String'];
};


export type UserAliasMutationDeleteArgs = {
  id: Scalars['String'];
};


export type UserAliasMutationUpdateArgs = {
  alias: Scalars['String'];
  id: Scalars['String'];
};

export type UserAliasQuery = {
  __typename?: 'UserAliasQuery';
  getForUser?: Maybe<PaginationListUserAliasGraphType>;
};


export type UserAliasQueryGetForUserArgs = {
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
  userId: Scalars['ID'];
};

export type UserQuery = {
  __typename?: 'UserQuery';
  get?: Maybe<PaginationListUserGraphType>;
  getForCommunicationChannel?: Maybe<PaginationListUserGraphType>;
  getForGroup?: Maybe<PaginationListUserGraphType>;
};


export type UserQueryGetArgs = {
  excludeUsersFromChannelWithId?: InputMaybe<Scalars['String']>;
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
  searchTerm: Scalars['String'];
};


export type UserQueryGetForCommunicationChannelArgs = {
  channelId: Scalars['ID'];
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
};


export type UserQueryGetForGroupArgs = {
  groupId: Scalars['ID'];
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
};

export type CreateChannelCalendarEventMutationVariables = Exact<{
  name: Scalars['String'];
  desc: Scalars['String'];
  startsAt: Scalars['Date'];
  endsAt?: InputMaybe<Scalars['Date']>;
  channelId?: InputMaybe<Scalars['String']>;
  groupId?: InputMaybe<Scalars['String']>;
  eventTypeId: Scalars['String'];
  maximalParticipantsCount?: InputMaybe<Scalars['Int']>;
}>;


export type CreateChannelCalendarEventMutation = { __typename?: 'Mutation', communicationChannelCalendarEvents?: { __typename?: 'CalendarEventMutation', create?: { __typename?: 'CalendarEvent', id: string, startsAt: any, endsAt?: any | null | undefined, name: string, description: string, authorId: string, createdAt: any, modifiedAt?: any | null | undefined } | null | undefined } | null | undefined };

export type DeleteChannelCalendarEventMutationVariables = Exact<{
  id: Scalars['String'];
}>;


export type DeleteChannelCalendarEventMutation = { __typename?: 'Mutation', communicationChannelCalendarEvents?: { __typename?: 'CalendarEventMutation', delete?: boolean | null | undefined } | null | undefined };

export type AddUserToCommunicationChannelMutationVariables = Exact<{
  userId: Scalars['String'];
  channelId: Scalars['String'];
}>;


export type AddUserToCommunicationChannelMutation = { __typename?: 'Mutation', communicationChannels?: { __typename?: 'CommunicationChannelMutation', addUser?: boolean | null | undefined } | null | undefined };

export type CreateCommunicationChannelMutationVariables = Exact<{
  name: Scalars['String'];
  desc: Scalars['String'];
}>;


export type CreateCommunicationChannelMutation = { __typename?: 'Mutation', communicationChannels?: { __typename?: 'CommunicationChannelMutation', create?: { __typename?: 'CommunicationChannel', id: string } | null | undefined } | null | undefined };

export type RemoveUserFromCommunicationChannelMutationVariables = Exact<{
  userId: Scalars['String'];
  channelId: Scalars['String'];
}>;


export type RemoveUserFromCommunicationChannelMutation = { __typename?: 'Mutation', communicationChannels?: { __typename?: 'CommunicationChannelMutation', removeUser?: boolean | null | undefined } | null | undefined };

export type CreateMessageMutationVariables = Exact<{
  channelId: Scalars['String'];
  content: Scalars['String'];
}>;


export type CreateMessageMutation = { __typename?: 'Mutation', communicationChannelMessages?: { __typename?: 'CommunicationChannelMessageMutation', create?: { __typename?: 'CommunicationChannelMessage', id: string, content: string, userName: string, type?: CommunicationChannelMessageType | null | undefined, channelId: string, createdAt: any, modifiedAt?: any | null | undefined } | null | undefined } | null | undefined };

export type GetCalendarEventsForChannelQueryVariables = Exact<{
  channelId: Scalars['String'];
}>;


export type GetCalendarEventsForChannelQuery = { __typename?: 'Query', communicationChannelCalendarEvents?: { __typename?: 'CommunicationChannelCalendarEventQuery', getForCommunicationChannel?: { __typename?: 'PaginationListCalendarEventGraphType', hasNextPage: boolean, hasPreviousPage: boolean, pageIndex: number, totalCount: number, totalPages: number, data?: Array<{ __typename?: 'CalendarEvent', id: string, startsAt: any, endsAt?: any | null | undefined, name: string, description: string, authorId: string, createdAt: any, modifiedAt?: any | null | undefined } | null | undefined> | null | undefined } | null | undefined } | null | undefined };

export type GetChannelCalendarEventQueryVariables = Exact<{
  id: Scalars['ID'];
}>;


export type GetChannelCalendarEventQuery = { __typename?: 'Query', communicationChannelCalendarEvents?: { __typename?: 'CommunicationChannelCalendarEventQuery', get?: { __typename?: 'CalendarEvent', id: string, startsAt: any, endsAt?: any | null | undefined, name: string, description: string, authorId: string, createdAt: any, modifiedAt?: any | null | undefined } | null | undefined } | null | undefined };

export type GetChannelsForUserQueryVariables = Exact<{
  userId: Scalars['String'];
  pageNumber: Scalars['Int'];
  pageSize: Scalars['Int'];
}>;


export type GetChannelsForUserQuery = { __typename?: 'Query', communicationChannels?: { __typename?: 'CommunicationChannelQuery', getForUser?: { __typename?: 'PaginationListCommunicationChannelGraphType', hasNextPage: boolean, hasPreviousPage: boolean, pageIndex: number, totalCount: number, totalPages: number, data?: Array<{ __typename?: 'CommunicationChannel', id: string, name: string, description: string, createdAt: any, modifiedAt?: any | null | undefined } | null | undefined> | null | undefined } | null | undefined } | null | undefined };

export type GetMessagesForChannelQueryVariables = Exact<{
  channelId: Scalars['ID'];
  pageNumber: Scalars['Int'];
  pageSize: Scalars['Int'];
}>;


export type GetMessagesForChannelQuery = { __typename?: 'Query', communicationChannelMessages?: { __typename?: 'CommunicationChannelMessageQuery', getForChannel?: { __typename?: 'PaginationListCommunicationChannelMessageGraphType', hasNextPage: boolean, hasPreviousPage: boolean, pageIndex: number, totalCount: number, totalPages: number, data?: Array<{ __typename?: 'CommunicationChannelMessage', id: string, content: string, userName: string, type?: CommunicationChannelMessageType | null | undefined, userId: string, channelId: string, createdAt: any, modifiedAt?: any | null | undefined } | null | undefined> | null | undefined } | null | undefined } | null | undefined };

export type GetUsersQueryVariables = Exact<{
  searchTerm: Scalars['String'];
  excludeUsersFromChannelWithId?: InputMaybe<Scalars['String']>;
}>;


export type GetUsersQuery = { __typename?: 'Query', users?: { __typename?: 'UserQuery', get?: { __typename?: 'PaginationListUserGraphType', hasNextPage: boolean, hasPreviousPage: boolean, pageIndex: number, totalCount: number, totalPages: number, data?: Array<{ __typename?: 'User', id: string, userName: string } | null | undefined> | null | undefined } | null | undefined } | null | undefined };

export type GetUsersForChannelQueryVariables = Exact<{
  channelId: Scalars['ID'];
}>;


export type GetUsersForChannelQuery = { __typename?: 'Query', users?: { __typename?: 'UserQuery', getForCommunicationChannel?: { __typename?: 'PaginationListUserGraphType', hasNextPage: boolean, hasPreviousPage: boolean, pageIndex: number, totalCount: number, totalPages: number, data?: Array<{ __typename?: 'User', id: string, userName: string, createdAt: any, modifiedAt?: any | null | undefined } | null | undefined> | null | undefined } | null | undefined } | null | undefined };

export type UserAddedToChannelSubscriptionVariables = Exact<{
  userId: Scalars['String'];
}>;


export type UserAddedToChannelSubscription = { __typename?: 'Subscription', communicationChannelAddedForUser?: { __typename?: 'CommunicationChannel', id: string, name: string, description: string, createdAt: any, modifiedAt?: any | null | undefined } | null | undefined };

export type MessageAddedToChannelSubscriptionVariables = Exact<{
  channelId: Scalars['String'];
}>;


export type MessageAddedToChannelSubscription = { __typename?: 'Subscription', communicationChannelMessageAddedToChannel?: { __typename?: 'CommunicationChannelMessage', id: string, createdAt: any, modifiedAt?: any | null | undefined, userName: string, content: string, channelId: string, userId: string, type?: CommunicationChannelMessageType | null | undefined } | null | undefined };


export const CreateChannelCalendarEventDocument = gql`
    mutation CreateChannelCalendarEvent($name: String!, $desc: String!, $startsAt: Date!, $endsAt: Date, $channelId: String, $groupId: String, $eventTypeId: String!, $maximalParticipantsCount: Int) {
  communicationChannelCalendarEvents {
    create(
      name: $name
      desc: $desc
      startsAt: $startsAt
      endsAt: $endsAt
      channelId: $channelId
      groupId: $groupId
      eventTypeId: $eventTypeId
      maximalParticipantsCount: $maximalParticipantsCount
    ) {
      id
      startsAt
      endsAt
      name
      description
      authorId
      createdAt
      modifiedAt
    }
  }
}
    `;
export type CreateChannelCalendarEventMutationFn = Apollo.MutationFunction<CreateChannelCalendarEventMutation, CreateChannelCalendarEventMutationVariables>;

/**
 * __useCreateChannelCalendarEventMutation__
 *
 * To run a mutation, you first call `useCreateChannelCalendarEventMutation` within a React component and pass it any options that fit your needs.
 * When your component renders, `useCreateChannelCalendarEventMutation` returns a tuple that includes:
 * - A mutate function that you can call at any time to execute the mutation
 * - An object with fields that represent the current status of the mutation's execution
 *
 * @param baseOptions options that will be passed into the mutation, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options-2;
 *
 * @example
 * const [createChannelCalendarEventMutation, { data, loading, error }] = useCreateChannelCalendarEventMutation({
 *   variables: {
 *      name: // value for 'name'
 *      desc: // value for 'desc'
 *      startsAt: // value for 'startsAt'
 *      endsAt: // value for 'endsAt'
 *      channelId: // value for 'channelId'
 *      groupId: // value for 'groupId'
 *      eventTypeId: // value for 'eventTypeId'
 *      maximalParticipantsCount: // value for 'maximalParticipantsCount'
 *   },
 * });
 */
export function useCreateChannelCalendarEventMutation(baseOptions?: Apollo.MutationHookOptions<CreateChannelCalendarEventMutation, CreateChannelCalendarEventMutationVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useMutation<CreateChannelCalendarEventMutation, CreateChannelCalendarEventMutationVariables>(CreateChannelCalendarEventDocument, options);
      }
export type CreateChannelCalendarEventMutationHookResult = ReturnType<typeof useCreateChannelCalendarEventMutation>;
export type CreateChannelCalendarEventMutationResult = Apollo.MutationResult<CreateChannelCalendarEventMutation>;
export type CreateChannelCalendarEventMutationOptions = Apollo.BaseMutationOptions<CreateChannelCalendarEventMutation, CreateChannelCalendarEventMutationVariables>;
export const DeleteChannelCalendarEventDocument = gql`
    mutation DeleteChannelCalendarEvent($id: String!) {
  communicationChannelCalendarEvents {
    delete(id: $id)
  }
}
    `;
export type DeleteChannelCalendarEventMutationFn = Apollo.MutationFunction<DeleteChannelCalendarEventMutation, DeleteChannelCalendarEventMutationVariables>;

/**
 * __useDeleteChannelCalendarEventMutation__
 *
 * To run a mutation, you first call `useDeleteChannelCalendarEventMutation` within a React component and pass it any options that fit your needs.
 * When your component renders, `useDeleteChannelCalendarEventMutation` returns a tuple that includes:
 * - A mutate function that you can call at any time to execute the mutation
 * - An object with fields that represent the current status of the mutation's execution
 *
 * @param baseOptions options that will be passed into the mutation, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options-2;
 *
 * @example
 * const [deleteChannelCalendarEventMutation, { data, loading, error }] = useDeleteChannelCalendarEventMutation({
 *   variables: {
 *      id: // value for 'id'
 *   },
 * });
 */
export function useDeleteChannelCalendarEventMutation(baseOptions?: Apollo.MutationHookOptions<DeleteChannelCalendarEventMutation, DeleteChannelCalendarEventMutationVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useMutation<DeleteChannelCalendarEventMutation, DeleteChannelCalendarEventMutationVariables>(DeleteChannelCalendarEventDocument, options);
      }
export type DeleteChannelCalendarEventMutationHookResult = ReturnType<typeof useDeleteChannelCalendarEventMutation>;
export type DeleteChannelCalendarEventMutationResult = Apollo.MutationResult<DeleteChannelCalendarEventMutation>;
export type DeleteChannelCalendarEventMutationOptions = Apollo.BaseMutationOptions<DeleteChannelCalendarEventMutation, DeleteChannelCalendarEventMutationVariables>;
export const AddUserToCommunicationChannelDocument = gql`
    mutation AddUserToCommunicationChannel($userId: String!, $channelId: String!) {
  communicationChannels {
    addUser(userId: $userId, channelId: $channelId)
  }
}
    `;
export type AddUserToCommunicationChannelMutationFn = Apollo.MutationFunction<AddUserToCommunicationChannelMutation, AddUserToCommunicationChannelMutationVariables>;

/**
 * __useAddUserToCommunicationChannelMutation__
 *
 * To run a mutation, you first call `useAddUserToCommunicationChannelMutation` within a React component and pass it any options that fit your needs.
 * When your component renders, `useAddUserToCommunicationChannelMutation` returns a tuple that includes:
 * - A mutate function that you can call at any time to execute the mutation
 * - An object with fields that represent the current status of the mutation's execution
 *
 * @param baseOptions options that will be passed into the mutation, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options-2;
 *
 * @example
 * const [addUserToCommunicationChannelMutation, { data, loading, error }] = useAddUserToCommunicationChannelMutation({
 *   variables: {
 *      userId: // value for 'userId'
 *      channelId: // value for 'channelId'
 *   },
 * });
 */
export function useAddUserToCommunicationChannelMutation(baseOptions?: Apollo.MutationHookOptions<AddUserToCommunicationChannelMutation, AddUserToCommunicationChannelMutationVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useMutation<AddUserToCommunicationChannelMutation, AddUserToCommunicationChannelMutationVariables>(AddUserToCommunicationChannelDocument, options);
      }
export type AddUserToCommunicationChannelMutationHookResult = ReturnType<typeof useAddUserToCommunicationChannelMutation>;
export type AddUserToCommunicationChannelMutationResult = Apollo.MutationResult<AddUserToCommunicationChannelMutation>;
export type AddUserToCommunicationChannelMutationOptions = Apollo.BaseMutationOptions<AddUserToCommunicationChannelMutation, AddUserToCommunicationChannelMutationVariables>;
export const CreateCommunicationChannelDocument = gql`
    mutation CreateCommunicationChannel($name: String!, $desc: String!) {
  communicationChannels {
    create(name: $name, desc: $desc) {
      id
    }
  }
}
    `;
export type CreateCommunicationChannelMutationFn = Apollo.MutationFunction<CreateCommunicationChannelMutation, CreateCommunicationChannelMutationVariables>;

/**
 * __useCreateCommunicationChannelMutation__
 *
 * To run a mutation, you first call `useCreateCommunicationChannelMutation` within a React component and pass it any options that fit your needs.
 * When your component renders, `useCreateCommunicationChannelMutation` returns a tuple that includes:
 * - A mutate function that you can call at any time to execute the mutation
 * - An object with fields that represent the current status of the mutation's execution
 *
 * @param baseOptions options that will be passed into the mutation, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options-2;
 *
 * @example
 * const [createCommunicationChannelMutation, { data, loading, error }] = useCreateCommunicationChannelMutation({
 *   variables: {
 *      name: // value for 'name'
 *      desc: // value for 'desc'
 *   },
 * });
 */
export function useCreateCommunicationChannelMutation(baseOptions?: Apollo.MutationHookOptions<CreateCommunicationChannelMutation, CreateCommunicationChannelMutationVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useMutation<CreateCommunicationChannelMutation, CreateCommunicationChannelMutationVariables>(CreateCommunicationChannelDocument, options);
      }
export type CreateCommunicationChannelMutationHookResult = ReturnType<typeof useCreateCommunicationChannelMutation>;
export type CreateCommunicationChannelMutationResult = Apollo.MutationResult<CreateCommunicationChannelMutation>;
export type CreateCommunicationChannelMutationOptions = Apollo.BaseMutationOptions<CreateCommunicationChannelMutation, CreateCommunicationChannelMutationVariables>;
export const RemoveUserFromCommunicationChannelDocument = gql`
    mutation RemoveUserFromCommunicationChannel($userId: String!, $channelId: String!) {
  communicationChannels {
    removeUser(userId: $userId, channelId: $channelId)
  }
}
    `;
export type RemoveUserFromCommunicationChannelMutationFn = Apollo.MutationFunction<RemoveUserFromCommunicationChannelMutation, RemoveUserFromCommunicationChannelMutationVariables>;

/**
 * __useRemoveUserFromCommunicationChannelMutation__
 *
 * To run a mutation, you first call `useRemoveUserFromCommunicationChannelMutation` within a React component and pass it any options that fit your needs.
 * When your component renders, `useRemoveUserFromCommunicationChannelMutation` returns a tuple that includes:
 * - A mutate function that you can call at any time to execute the mutation
 * - An object with fields that represent the current status of the mutation's execution
 *
 * @param baseOptions options that will be passed into the mutation, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options-2;
 *
 * @example
 * const [removeUserFromCommunicationChannelMutation, { data, loading, error }] = useRemoveUserFromCommunicationChannelMutation({
 *   variables: {
 *      userId: // value for 'userId'
 *      channelId: // value for 'channelId'
 *   },
 * });
 */
export function useRemoveUserFromCommunicationChannelMutation(baseOptions?: Apollo.MutationHookOptions<RemoveUserFromCommunicationChannelMutation, RemoveUserFromCommunicationChannelMutationVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useMutation<RemoveUserFromCommunicationChannelMutation, RemoveUserFromCommunicationChannelMutationVariables>(RemoveUserFromCommunicationChannelDocument, options);
      }
export type RemoveUserFromCommunicationChannelMutationHookResult = ReturnType<typeof useRemoveUserFromCommunicationChannelMutation>;
export type RemoveUserFromCommunicationChannelMutationResult = Apollo.MutationResult<RemoveUserFromCommunicationChannelMutation>;
export type RemoveUserFromCommunicationChannelMutationOptions = Apollo.BaseMutationOptions<RemoveUserFromCommunicationChannelMutation, RemoveUserFromCommunicationChannelMutationVariables>;
export const CreateMessageDocument = gql`
    mutation CreateMessage($channelId: String!, $content: String!) {
  communicationChannelMessages {
    create(channelId: $channelId, content: $content, type: 1) {
      id
      content
      userName
      type
      channelId
      createdAt
      modifiedAt
    }
  }
}
    `;
export type CreateMessageMutationFn = Apollo.MutationFunction<CreateMessageMutation, CreateMessageMutationVariables>;

/**
 * __useCreateMessageMutation__
 *
 * To run a mutation, you first call `useCreateMessageMutation` within a React component and pass it any options that fit your needs.
 * When your component renders, `useCreateMessageMutation` returns a tuple that includes:
 * - A mutate function that you can call at any time to execute the mutation
 * - An object with fields that represent the current status of the mutation's execution
 *
 * @param baseOptions options that will be passed into the mutation, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options-2;
 *
 * @example
 * const [createMessageMutation, { data, loading, error }] = useCreateMessageMutation({
 *   variables: {
 *      channelId: // value for 'channelId'
 *      content: // value for 'content'
 *   },
 * });
 */
export function useCreateMessageMutation(baseOptions?: Apollo.MutationHookOptions<CreateMessageMutation, CreateMessageMutationVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useMutation<CreateMessageMutation, CreateMessageMutationVariables>(CreateMessageDocument, options);
      }
export type CreateMessageMutationHookResult = ReturnType<typeof useCreateMessageMutation>;
export type CreateMessageMutationResult = Apollo.MutationResult<CreateMessageMutation>;
export type CreateMessageMutationOptions = Apollo.BaseMutationOptions<CreateMessageMutation, CreateMessageMutationVariables>;
export const GetCalendarEventsForChannelDocument = gql`
    query GetCalendarEventsForChannel($channelId: String!) {
  communicationChannelCalendarEvents {
    getForCommunicationChannel(channelId: $channelId) {
      data {
        id
        startsAt
        endsAt
        name
        description
        authorId
        createdAt
        modifiedAt
      }
      hasNextPage
      hasPreviousPage
      pageIndex
      totalCount
      totalPages
    }
  }
}
    `;

/**
 * __useGetCalendarEventsForChannelQuery__
 *
 * To run a query within a React component, call `useGetCalendarEventsForChannelQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetCalendarEventsForChannelQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetCalendarEventsForChannelQuery({
 *   variables: {
 *      channelId: // value for 'channelId'
 *   },
 * });
 */
export function useGetCalendarEventsForChannelQuery(baseOptions: Apollo.QueryHookOptions<GetCalendarEventsForChannelQuery, GetCalendarEventsForChannelQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetCalendarEventsForChannelQuery, GetCalendarEventsForChannelQueryVariables>(GetCalendarEventsForChannelDocument, options);
      }
export function useGetCalendarEventsForChannelLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetCalendarEventsForChannelQuery, GetCalendarEventsForChannelQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetCalendarEventsForChannelQuery, GetCalendarEventsForChannelQueryVariables>(GetCalendarEventsForChannelDocument, options);
        }
export type GetCalendarEventsForChannelQueryHookResult = ReturnType<typeof useGetCalendarEventsForChannelQuery>;
export type GetCalendarEventsForChannelLazyQueryHookResult = ReturnType<typeof useGetCalendarEventsForChannelLazyQuery>;
export type GetCalendarEventsForChannelQueryResult = Apollo.QueryResult<GetCalendarEventsForChannelQuery, GetCalendarEventsForChannelQueryVariables>;
export const GetChannelCalendarEventDocument = gql`
    query GetChannelCalendarEvent($id: ID!) {
  communicationChannelCalendarEvents {
    get(id: $id) {
      id
      startsAt
      endsAt
      name
      description
      authorId
      createdAt
      modifiedAt
    }
  }
}
    `;

/**
 * __useGetChannelCalendarEventQuery__
 *
 * To run a query within a React component, call `useGetChannelCalendarEventQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetChannelCalendarEventQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetChannelCalendarEventQuery({
 *   variables: {
 *      id: // value for 'id'
 *   },
 * });
 */
export function useGetChannelCalendarEventQuery(baseOptions: Apollo.QueryHookOptions<GetChannelCalendarEventQuery, GetChannelCalendarEventQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetChannelCalendarEventQuery, GetChannelCalendarEventQueryVariables>(GetChannelCalendarEventDocument, options);
      }
export function useGetChannelCalendarEventLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetChannelCalendarEventQuery, GetChannelCalendarEventQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetChannelCalendarEventQuery, GetChannelCalendarEventQueryVariables>(GetChannelCalendarEventDocument, options);
        }
export type GetChannelCalendarEventQueryHookResult = ReturnType<typeof useGetChannelCalendarEventQuery>;
export type GetChannelCalendarEventLazyQueryHookResult = ReturnType<typeof useGetChannelCalendarEventLazyQuery>;
export type GetChannelCalendarEventQueryResult = Apollo.QueryResult<GetChannelCalendarEventQuery, GetChannelCalendarEventQueryVariables>;
export const GetChannelsForUserDocument = gql`
    query GetChannelsForUser($userId: String!, $pageNumber: Int!, $pageSize: Int!) {
  communicationChannels {
    getForUser(userId: $userId, pageNumber: $pageNumber, pageSize: $pageSize) {
      data {
        id
        name
        description
        createdAt
        modifiedAt
      }
      hasNextPage
      hasPreviousPage
      pageIndex
      totalCount
      totalPages
    }
  }
}
    `;

/**
 * __useGetChannelsForUserQuery__
 *
 * To run a query within a React component, call `useGetChannelsForUserQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetChannelsForUserQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetChannelsForUserQuery({
 *   variables: {
 *      userId: // value for 'userId'
 *      pageNumber: // value for 'pageNumber'
 *      pageSize: // value for 'pageSize'
 *   },
 * });
 */
export function useGetChannelsForUserQuery(baseOptions: Apollo.QueryHookOptions<GetChannelsForUserQuery, GetChannelsForUserQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetChannelsForUserQuery, GetChannelsForUserQueryVariables>(GetChannelsForUserDocument, options);
      }
export function useGetChannelsForUserLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetChannelsForUserQuery, GetChannelsForUserQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetChannelsForUserQuery, GetChannelsForUserQueryVariables>(GetChannelsForUserDocument, options);
        }
export type GetChannelsForUserQueryHookResult = ReturnType<typeof useGetChannelsForUserQuery>;
export type GetChannelsForUserLazyQueryHookResult = ReturnType<typeof useGetChannelsForUserLazyQuery>;
export type GetChannelsForUserQueryResult = Apollo.QueryResult<GetChannelsForUserQuery, GetChannelsForUserQueryVariables>;
export const GetMessagesForChannelDocument = gql`
    query GetMessagesForChannel($channelId: ID!, $pageNumber: Int!, $pageSize: Int!) {
  communicationChannelMessages {
    getForChannel(
      channelId: $channelId
      pageNumber: $pageNumber
      pageSize: $pageSize
    ) {
      data {
        id
        content
        userName
        type
        userId
        channelId
        createdAt
        modifiedAt
      }
      hasNextPage
      hasPreviousPage
      pageIndex
      totalCount
      totalPages
    }
  }
}
    `;

/**
 * __useGetMessagesForChannelQuery__
 *
 * To run a query within a React component, call `useGetMessagesForChannelQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetMessagesForChannelQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetMessagesForChannelQuery({
 *   variables: {
 *      channelId: // value for 'channelId'
 *      pageNumber: // value for 'pageNumber'
 *      pageSize: // value for 'pageSize'
 *   },
 * });
 */
export function useGetMessagesForChannelQuery(baseOptions: Apollo.QueryHookOptions<GetMessagesForChannelQuery, GetMessagesForChannelQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetMessagesForChannelQuery, GetMessagesForChannelQueryVariables>(GetMessagesForChannelDocument, options);
      }
export function useGetMessagesForChannelLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetMessagesForChannelQuery, GetMessagesForChannelQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetMessagesForChannelQuery, GetMessagesForChannelQueryVariables>(GetMessagesForChannelDocument, options);
        }
export type GetMessagesForChannelQueryHookResult = ReturnType<typeof useGetMessagesForChannelQuery>;
export type GetMessagesForChannelLazyQueryHookResult = ReturnType<typeof useGetMessagesForChannelLazyQuery>;
export type GetMessagesForChannelQueryResult = Apollo.QueryResult<GetMessagesForChannelQuery, GetMessagesForChannelQueryVariables>;
export const GetUsersDocument = gql`
    query GetUsers($searchTerm: String!, $excludeUsersFromChannelWithId: String) {
  users {
    get(
      searchTerm: $searchTerm
      excludeUsersFromChannelWithId: $excludeUsersFromChannelWithId
    ) {
      data {
        id
        userName
      }
      hasNextPage
      hasPreviousPage
      pageIndex
      totalCount
      totalPages
    }
  }
}
    `;

/**
 * __useGetUsersQuery__
 *
 * To run a query within a React component, call `useGetUsersQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetUsersQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetUsersQuery({
 *   variables: {
 *      searchTerm: // value for 'searchTerm'
 *      excludeUsersFromChannelWithId: // value for 'excludeUsersFromChannelWithId'
 *   },
 * });
 */
export function useGetUsersQuery(baseOptions: Apollo.QueryHookOptions<GetUsersQuery, GetUsersQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetUsersQuery, GetUsersQueryVariables>(GetUsersDocument, options);
      }
export function useGetUsersLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetUsersQuery, GetUsersQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetUsersQuery, GetUsersQueryVariables>(GetUsersDocument, options);
        }
export type GetUsersQueryHookResult = ReturnType<typeof useGetUsersQuery>;
export type GetUsersLazyQueryHookResult = ReturnType<typeof useGetUsersLazyQuery>;
export type GetUsersQueryResult = Apollo.QueryResult<GetUsersQuery, GetUsersQueryVariables>;
export const GetUsersForChannelDocument = gql`
    query GetUsersForChannel($channelId: ID!) {
  users {
    getForCommunicationChannel(channelId: $channelId) {
      data {
        id
        userName
        createdAt
        modifiedAt
      }
      hasNextPage
      hasPreviousPage
      pageIndex
      totalCount
      totalPages
    }
  }
}
    `;

/**
 * __useGetUsersForChannelQuery__
 *
 * To run a query within a React component, call `useGetUsersForChannelQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetUsersForChannelQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetUsersForChannelQuery({
 *   variables: {
 *      channelId: // value for 'channelId'
 *   },
 * });
 */
export function useGetUsersForChannelQuery(baseOptions: Apollo.QueryHookOptions<GetUsersForChannelQuery, GetUsersForChannelQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetUsersForChannelQuery, GetUsersForChannelQueryVariables>(GetUsersForChannelDocument, options);
      }
export function useGetUsersForChannelLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetUsersForChannelQuery, GetUsersForChannelQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetUsersForChannelQuery, GetUsersForChannelQueryVariables>(GetUsersForChannelDocument, options);
        }
export type GetUsersForChannelQueryHookResult = ReturnType<typeof useGetUsersForChannelQuery>;
export type GetUsersForChannelLazyQueryHookResult = ReturnType<typeof useGetUsersForChannelLazyQuery>;
export type GetUsersForChannelQueryResult = Apollo.QueryResult<GetUsersForChannelQuery, GetUsersForChannelQueryVariables>;
export const UserAddedToChannelDocument = gql`
    subscription UserAddedToChannel($userId: String!) {
  communicationChannelAddedForUser(userId: $userId) {
    id
    name
    description
    createdAt
    modifiedAt
  }
}
    `;

/**
 * __useUserAddedToChannelSubscription__
 *
 * To run a query within a React component, call `useUserAddedToChannelSubscription` and pass it any options that fit your needs.
 * When your component renders, `useUserAddedToChannelSubscription` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the subscription, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useUserAddedToChannelSubscription({
 *   variables: {
 *      userId: // value for 'userId'
 *   },
 * });
 */
export function useUserAddedToChannelSubscription(baseOptions: Apollo.SubscriptionHookOptions<UserAddedToChannelSubscription, UserAddedToChannelSubscriptionVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useSubscription<UserAddedToChannelSubscription, UserAddedToChannelSubscriptionVariables>(UserAddedToChannelDocument, options);
      }
export type UserAddedToChannelSubscriptionHookResult = ReturnType<typeof useUserAddedToChannelSubscription>;
export type UserAddedToChannelSubscriptionResult = Apollo.SubscriptionResult<UserAddedToChannelSubscription>;
export const MessageAddedToChannelDocument = gql`
    subscription MessageAddedToChannel($channelId: String!) {
  communicationChannelMessageAddedToChannel(channelId: $channelId) {
    id
    createdAt
    modifiedAt
    userName
    content
    channelId
    userId
    type
  }
}
    `;

/**
 * __useMessageAddedToChannelSubscription__
 *
 * To run a query within a React component, call `useMessageAddedToChannelSubscription` and pass it any options that fit your needs.
 * When your component renders, `useMessageAddedToChannelSubscription` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the subscription, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useMessageAddedToChannelSubscription({
 *   variables: {
 *      channelId: // value for 'channelId'
 *   },
 * });
 */
export function useMessageAddedToChannelSubscription(baseOptions: Apollo.SubscriptionHookOptions<MessageAddedToChannelSubscription, MessageAddedToChannelSubscriptionVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useSubscription<MessageAddedToChannelSubscription, MessageAddedToChannelSubscriptionVariables>(MessageAddedToChannelDocument, options);
      }
export type MessageAddedToChannelSubscriptionHookResult = ReturnType<typeof useMessageAddedToChannelSubscription>;
export type MessageAddedToChannelSubscriptionResult = Apollo.SubscriptionResult<MessageAddedToChannelSubscription>;