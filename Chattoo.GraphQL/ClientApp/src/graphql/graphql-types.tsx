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
  /** The `DateTime` scalar type represents a date and time. `DateTime` expects timestamps to be formatted in accordance with the [ISO-8601](https://en.wikipedia.org/wiki/ISO_8601) standard. */
  DateTime: any;
};

export type CalendarEvent = {
  __typename?: 'CalendarEvent';
  authorId: Scalars['String'];
  communicationChannelId?: Maybe<Scalars['String']>;
  createdAt?: Maybe<Scalars['DateTime']>;
  createdBy?: Maybe<Scalars['String']>;
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy?: Maybe<Scalars['String']>;
  description: Scalars['String'];
  endsAt?: Maybe<Scalars['DateTime']>;
  groupId?: Maybe<Scalars['String']>;
  id: Scalars['String'];
  maximalParticipantsCount?: Maybe<Scalars['Int']>;
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy?: Maybe<Scalars['String']>;
  name: Scalars['String'];
  participantsCount: Scalars['Int'];
  startsAt: Scalars['DateTime'];
  type?: Maybe<CalendarEventTypeGraphType>;
};

export type CalendarEventMutation = {
  __typename?: 'CalendarEventMutation';
  addUser?: Maybe<Scalars['Boolean']>;
  create?: Maybe<CalendarEvent>;
  delete?: Maybe<Scalars['Boolean']>;
  removeUser?: Maybe<Scalars['Boolean']>;
  update?: Maybe<Scalars['Boolean']>;
};


export type CalendarEventMutationAddUserArgs = {
  eventId: Scalars['String'];
  userId: Scalars['String'];
};


export type CalendarEventMutationCreateArgs = {
  channelId?: InputMaybe<Scalars['String']>;
  desc: Scalars['String'];
  endsAt?: InputMaybe<Scalars['DateTime']>;
  groupId?: InputMaybe<Scalars['String']>;
  maximalParticipantsCount?: InputMaybe<Scalars['Int']>;
  name: Scalars['String'];
  startsAt: Scalars['DateTime'];
  type: Scalars['String'];
};


export type CalendarEventMutationDeleteArgs = {
  id: Scalars['ID'];
};


export type CalendarEventMutationRemoveUserArgs = {
  eventId: Scalars['String'];
  userId: Scalars['String'];
};


export type CalendarEventMutationUpdateArgs = {
  desc: Scalars['String'];
  endsAt: Scalars['DateTime'];
  id: Scalars['String'];
  maximalParticipantsCount?: InputMaybe<Scalars['Int']>;
  name: Scalars['String'];
  startsAt?: InputMaybe<Scalars['DateTime']>;
};

export type CalendarEventQuery = {
  __typename?: 'CalendarEventQuery';
  get?: Maybe<CalendarEvent>;
  getAvailable?: Maybe<CalendarEvent>;
  getForCommunicationChannel?: Maybe<PaginationListCalendarEventGraphType>;
  getForGroup?: Maybe<PaginationListCalendarEventGraphType>;
  getJoined?: Maybe<PaginationListCalendarEventGraphType>;
  getVisible?: Maybe<PaginationListCalendarEventGraphType>;
};


export type CalendarEventQueryGetArgs = {
  id: Scalars['ID'];
};


export type CalendarEventQueryGetForCommunicationChannelArgs = {
  channelId: Scalars['String'];
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
};


export type CalendarEventQueryGetForGroupArgs = {
  groupId: Scalars['String'];
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
};


export type CalendarEventQueryGetJoinedArgs = {
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
};


export type CalendarEventQueryGetVisibleArgs = {
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
};

export enum CalendarEventTypeGraphType {
  Brainstorming = 'BRAINSTORMING',
  Celebration = 'CELEBRATION',
  Cinema = 'CINEMA',
  Drink = 'DRINK',
  Sports = 'SPORTS',
  Theatre = 'THEATRE',
  Walk = 'WALK'
}

export type CalendarEventWish = {
  __typename?: 'CalendarEventWish';
  authorId: Scalars['String'];
  authorName: Scalars['String'];
  createdAt?: Maybe<Scalars['DateTime']>;
  createdBy?: Maybe<Scalars['String']>;
  dateIntervals?: Maybe<Array<Maybe<DateInterval>>>;
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy?: Maybe<Scalars['String']>;
  id: Scalars['String'];
  maximalParticipantsCount?: Maybe<Scalars['Int']>;
  minimalParticipantsCount?: Maybe<Scalars['Int']>;
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy?: Maybe<Scalars['String']>;
};

export type CalendarEventWishMutation = {
  __typename?: 'CalendarEventWishMutation';
  create?: Maybe<CalendarEventWish>;
  delete?: Maybe<Scalars['Boolean']>;
  update?: Maybe<Scalars['Boolean']>;
};


export type CalendarEventWishMutationCreateArgs = {
  channelId: Scalars['String'];
  dateIntervals: Array<InputMaybe<DateIntervalInput>>;
  minimalParticipantsCount: Scalars['Int'];
  type: Scalars['String'];
};


export type CalendarEventWishMutationDeleteArgs = {
  id: Scalars['ID'];
};


export type CalendarEventWishMutationUpdateArgs = {
  dateIntervals?: InputMaybe<Array<InputMaybe<DateIntervalInput>>>;
  id: Scalars['String'];
  minimalParticipantsCount?: InputMaybe<Scalars['Int']>;
  type: Scalars['String'];
};

export type CalendarEventWishQuery = {
  __typename?: 'CalendarEventWishQuery';
  getActive?: Maybe<PaginationListCalendarEventWishGraphType>;
};


export type CalendarEventWishQueryGetActiveArgs = {
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
};

export type CommunicationChannel = {
  __typename?: 'CommunicationChannel';
  createdAt?: Maybe<Scalars['DateTime']>;
  createdBy?: Maybe<Scalars['String']>;
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy?: Maybe<Scalars['String']>;
  description: Scalars['String'];
  id: Scalars['String'];
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy?: Maybe<Scalars['String']>;
  name: Scalars['String'];
};

export type CommunicationChannelMessage = {
  __typename?: 'CommunicationChannelMessage';
  channelId: Scalars['String'];
  content: Scalars['String'];
  createdAt?: Maybe<Scalars['DateTime']>;
  createdBy?: Maybe<Scalars['String']>;
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy?: Maybe<Scalars['String']>;
  id: Scalars['String'];
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy?: Maybe<Scalars['String']>;
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
  createdAt?: Maybe<Scalars['DateTime']>;
  createdBy?: Maybe<Scalars['String']>;
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy?: Maybe<Scalars['String']>;
  id: Scalars['String'];
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy?: Maybe<Scalars['String']>;
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

export type DateInterval = {
  __typename?: 'DateInterval';
  endsAt: Scalars['DateTime'];
  id: Scalars['String'];
  startsAt: Scalars['DateTime'];
};

export type DateIntervalInput = {
  endsAt: Scalars['DateTime'];
  startsAt: Scalars['DateTime'];
};

export type Group = {
  __typename?: 'Group';
  createdAt?: Maybe<Scalars['DateTime']>;
  createdBy?: Maybe<Scalars['String']>;
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy?: Maybe<Scalars['String']>;
  id: Scalars['String'];
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy?: Maybe<Scalars['String']>;
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
  createdAt?: Maybe<Scalars['DateTime']>;
  createdBy?: Maybe<Scalars['String']>;
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy?: Maybe<Scalars['String']>;
  id: Scalars['String'];
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy?: Maybe<Scalars['String']>;
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
  wishes?: Maybe<CalendarEventWishMutation>;
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

export type PaginationListCalendarEventWishGraphType = {
  __typename?: 'PaginationListCalendarEventWishGraphType';
  data?: Maybe<Array<Maybe<CalendarEventWish>>>;
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
  calendarEvents?: Maybe<CalendarEventQuery>;
  communicationChannelMessages?: Maybe<CommunicationChannelMessageQuery>;
  communicationChannelRoles?: Maybe<CommunicationChannelRoleQuery>;
  communicationChannels?: Maybe<CommunicationChannelQuery>;
  groupRoles?: Maybe<GroupRoleQuery>;
  groups?: Maybe<GroupQuery>;
  userAliases?: Maybe<UserAliasQuery>;
  users?: Maybe<UserQuery>;
  wishes?: Maybe<CalendarEventWishQuery>;
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
  createdAt?: Maybe<Scalars['DateTime']>;
  createdBy?: Maybe<Scalars['String']>;
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy?: Maybe<Scalars['String']>;
  id: Scalars['String'];
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy?: Maybe<Scalars['String']>;
  userName: Scalars['String'];
};

export type UserAlias = {
  __typename?: 'UserAlias';
  alias: Scalars['String'];
  createdAt?: Maybe<Scalars['DateTime']>;
  createdBy?: Maybe<Scalars['String']>;
  deletedAt?: Maybe<Scalars['DateTime']>;
  deletedBy?: Maybe<Scalars['String']>;
  id: Scalars['String'];
  modifiedAt?: Maybe<Scalars['DateTime']>;
  modifiedBy?: Maybe<Scalars['String']>;
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
  getForCalendarEvent?: Maybe<PaginationListUserGraphType>;
  getForCommunicationChannel?: Maybe<PaginationListUserGraphType>;
  getForGroup?: Maybe<PaginationListUserGraphType>;
};


export type UserQueryGetArgs = {
  channelId?: InputMaybe<Scalars['String']>;
  excludedUserIds?: InputMaybe<Array<InputMaybe<Scalars['String']>>>;
  groupId?: InputMaybe<Scalars['String']>;
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
  searchTerm: Scalars['String'];
};


export type UserQueryGetForCalendarEventArgs = {
  calendarEventId: Scalars['ID'];
  pageNumber?: Scalars['Int'];
  pageSize?: Scalars['Int'];
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

export type AddUserToCalendarEventMutationVariables = Exact<{
  userId: Scalars['String'];
  eventId: Scalars['String'];
}>;


export type AddUserToCalendarEventMutation = { __typename?: 'Mutation', communicationChannelCalendarEvents?: { __typename?: 'CalendarEventMutation', addUser?: boolean | null | undefined } | null | undefined };

export type CreateChannelCalendarEventMutationVariables = Exact<{
  name: Scalars['String'];
  desc: Scalars['String'];
  startsAt: Scalars['DateTime'];
  endsAt?: InputMaybe<Scalars['DateTime']>;
  channelId?: InputMaybe<Scalars['String']>;
  groupId?: InputMaybe<Scalars['String']>;
  type: Scalars['String'];
  maximalParticipantsCount?: InputMaybe<Scalars['Int']>;
}>;


export type CreateChannelCalendarEventMutation = { __typename?: 'Mutation', communicationChannelCalendarEvents?: { __typename?: 'CalendarEventMutation', create?: { __typename?: 'CalendarEvent', id: string, startsAt: any, endsAt?: any | null | undefined, name: string, description: string, authorId: string, createdAt?: any | null | undefined, modifiedAt?: any | null | undefined } | null | undefined } | null | undefined };

export type DeleteCalendarEventMutationVariables = Exact<{
  id: Scalars['ID'];
}>;


export type DeleteCalendarEventMutation = { __typename?: 'Mutation', communicationChannelCalendarEvents?: { __typename?: 'CalendarEventMutation', delete?: boolean | null | undefined } | null | undefined };

export type RemoveUserFromCalendarEventMutationVariables = Exact<{
  userId: Scalars['String'];
  eventId: Scalars['String'];
}>;


export type RemoveUserFromCalendarEventMutation = { __typename?: 'Mutation', communicationChannelCalendarEvents?: { __typename?: 'CalendarEventMutation', removeUser?: boolean | null | undefined } | null | undefined };

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


export type CreateMessageMutation = { __typename?: 'Mutation', communicationChannelMessages?: { __typename?: 'CommunicationChannelMessageMutation', create?: { __typename?: 'CommunicationChannelMessage', id: string, content: string, userName: string, type?: CommunicationChannelMessageType | null | undefined, channelId: string, createdAt?: any | null | undefined, modifiedAt?: any | null | undefined } | null | undefined } | null | undefined };

export type CreateWishMutationVariables = Exact<{
  channelId: Scalars['String'];
  type: Scalars['String'];
  minimalParticipantsCount: Scalars['Int'];
  dateIntervals: Array<DateIntervalInput> | DateIntervalInput;
}>;


export type CreateWishMutation = { __typename?: 'Mutation', wishes?: { __typename?: 'CalendarEventWishMutation', create?: { __typename?: 'CalendarEventWish', id: string, authorId: string, authorName: string, createdAt?: any | null | undefined, modifiedAt?: any | null | undefined, dateIntervals?: Array<{ __typename?: 'DateInterval', startsAt: any, endsAt: any } | null | undefined> | null | undefined } | null | undefined } | null | undefined };

export type DeleteWishMutationVariables = Exact<{
  id: Scalars['ID'];
}>;


export type DeleteWishMutation = { __typename?: 'Mutation', wishes?: { __typename?: 'CalendarEventWishMutation', delete?: boolean | null | undefined } | null | undefined };

export type GetCalendarEventQueryVariables = Exact<{
  id: Scalars['ID'];
}>;


export type GetCalendarEventQuery = { __typename?: 'Query', calendarEvents?: { __typename?: 'CalendarEventQuery', get?: { __typename?: 'CalendarEvent', id: string, startsAt: any, endsAt?: any | null | undefined, name: string, description: string, maximalParticipantsCount?: number | null | undefined, participantsCount: number, authorId: string, communicationChannelId?: string | null | undefined, createdAt?: any | null | undefined, modifiedAt?: any | null | undefined, createdBy?: string | null | undefined, deletedBy?: string | null | undefined, modifiedBy?: string | null | undefined, type?: CalendarEventTypeGraphType | null | undefined } | null | undefined } | null | undefined };

export type GetCalendarEventsQueryVariables = Exact<{
  pageNumber: Scalars['Int'];
  pageSize: Scalars['Int'];
}>;


export type GetCalendarEventsQuery = { __typename?: 'Query', calendarEvents?: { __typename?: 'CalendarEventQuery', getVisible?: { __typename?: 'PaginationListCalendarEventGraphType', hasNextPage: boolean, hasPreviousPage: boolean, pageIndex: number, totalCount: number, totalPages: number, data?: Array<{ __typename?: 'CalendarEvent', id: string, startsAt: any, endsAt?: any | null | undefined, name: string, description: string, maximalParticipantsCount?: number | null | undefined, participantsCount: number, authorId: string, communicationChannelId?: string | null | undefined, createdAt?: any | null | undefined, modifiedAt?: any | null | undefined, type?: CalendarEventTypeGraphType | null | undefined } | null | undefined> | null | undefined } | null | undefined } | null | undefined };

export type GetJoinedCalendarEventsQueryVariables = Exact<{
  pageNumber: Scalars['Int'];
  pageSize: Scalars['Int'];
}>;


export type GetJoinedCalendarEventsQuery = { __typename?: 'Query', calendarEvents?: { __typename?: 'CalendarEventQuery', getJoined?: { __typename?: 'PaginationListCalendarEventGraphType', hasNextPage: boolean, hasPreviousPage: boolean, pageIndex: number, totalCount: number, totalPages: number, data?: Array<{ __typename?: 'CalendarEvent', id: string, startsAt: any, endsAt?: any | null | undefined, name: string, description: string, maximalParticipantsCount?: number | null | undefined, participantsCount: number, authorId: string, communicationChannelId?: string | null | undefined, createdAt?: any | null | undefined, modifiedAt?: any | null | undefined, type?: CalendarEventTypeGraphType | null | undefined } | null | undefined> | null | undefined } | null | undefined } | null | undefined };

export type GetChannelQueryVariables = Exact<{
  id: Scalars['ID'];
}>;


export type GetChannelQuery = { __typename?: 'Query', communicationChannels?: { __typename?: 'CommunicationChannelQuery', get?: { __typename?: 'CommunicationChannel', id: string, name: string, description: string, createdAt?: any | null | undefined, modifiedAt?: any | null | undefined, createdBy?: string | null | undefined, deletedBy?: string | null | undefined, modifiedBy?: string | null | undefined } | null | undefined } | null | undefined };

export type GetChannelsForUserQueryVariables = Exact<{
  userId: Scalars['String'];
  pageNumber: Scalars['Int'];
  pageSize: Scalars['Int'];
}>;


export type GetChannelsForUserQuery = { __typename?: 'Query', communicationChannels?: { __typename?: 'CommunicationChannelQuery', getForUser?: { __typename?: 'PaginationListCommunicationChannelGraphType', hasNextPage: boolean, hasPreviousPage: boolean, pageIndex: number, totalCount: number, totalPages: number, data?: Array<{ __typename?: 'CommunicationChannel', id: string, name: string, description: string, createdAt?: any | null | undefined, modifiedAt?: any | null | undefined } | null | undefined> | null | undefined } | null | undefined } | null | undefined };

export type GetMessagesForChannelQueryVariables = Exact<{
  channelId: Scalars['ID'];
  pageNumber: Scalars['Int'];
  pageSize: Scalars['Int'];
}>;


export type GetMessagesForChannelQuery = { __typename?: 'Query', communicationChannelMessages?: { __typename?: 'CommunicationChannelMessageQuery', getForChannel?: { __typename?: 'PaginationListCommunicationChannelMessageGraphType', hasNextPage: boolean, hasPreviousPage: boolean, pageIndex: number, totalCount: number, totalPages: number, data?: Array<{ __typename?: 'CommunicationChannelMessage', id: string, content: string, userName: string, type?: CommunicationChannelMessageType | null | undefined, userId: string, channelId: string, createdAt?: any | null | undefined, modifiedAt?: any | null | undefined } | null | undefined> | null | undefined } | null | undefined } | null | undefined };

export type GetUsersQueryVariables = Exact<{
  searchTerm: Scalars['String'];
  excludedUserIds?: InputMaybe<Array<InputMaybe<Scalars['String']>> | InputMaybe<Scalars['String']>>;
  channelId?: InputMaybe<Scalars['String']>;
  groupId?: InputMaybe<Scalars['String']>;
}>;


export type GetUsersQuery = { __typename?: 'Query', users?: { __typename?: 'UserQuery', get?: { __typename?: 'PaginationListUserGraphType', hasNextPage: boolean, hasPreviousPage: boolean, pageIndex: number, totalCount: number, totalPages: number, data?: Array<{ __typename?: 'User', id: string, userName: string, createdAt?: any | null | undefined, modifiedAt?: any | null | undefined, createdBy?: string | null | undefined, deletedBy?: string | null | undefined, modifiedBy?: string | null | undefined } | null | undefined> | null | undefined } | null | undefined } | null | undefined };

export type GetUsersForCalendarEventQueryVariables = Exact<{
  calendarEventId: Scalars['ID'];
}>;


export type GetUsersForCalendarEventQuery = { __typename?: 'Query', users?: { __typename?: 'UserQuery', getForCalendarEvent?: { __typename?: 'PaginationListUserGraphType', hasNextPage: boolean, hasPreviousPage: boolean, pageIndex: number, totalCount: number, totalPages: number, data?: Array<{ __typename?: 'User', id: string, userName: string, createdAt?: any | null | undefined, modifiedAt?: any | null | undefined, createdBy?: string | null | undefined, deletedBy?: string | null | undefined, modifiedBy?: string | null | undefined } | null | undefined> | null | undefined } | null | undefined } | null | undefined };

export type GetUsersForChannelQueryVariables = Exact<{
  channelId: Scalars['ID'];
}>;


export type GetUsersForChannelQuery = { __typename?: 'Query', users?: { __typename?: 'UserQuery', getForCommunicationChannel?: { __typename?: 'PaginationListUserGraphType', hasNextPage: boolean, hasPreviousPage: boolean, pageIndex: number, totalCount: number, totalPages: number, data?: Array<{ __typename?: 'User', id: string, userName: string, createdAt?: any | null | undefined, modifiedAt?: any | null | undefined } | null | undefined> | null | undefined } | null | undefined } | null | undefined };

export type GetActiveWishesQueryVariables = Exact<{
  pageNumber: Scalars['Int'];
  pageSize: Scalars['Int'];
}>;


export type GetActiveWishesQuery = { __typename?: 'Query', wishes?: { __typename?: 'CalendarEventWishQuery', getActive?: { __typename?: 'PaginationListCalendarEventWishGraphType', hasNextPage: boolean, hasPreviousPage: boolean, pageIndex: number, totalCount: number, totalPages: number, data?: Array<{ __typename?: 'CalendarEventWish', id: string, authorId: string, authorName: string, maximalParticipantsCount?: number | null | undefined, minimalParticipantsCount?: number | null | undefined, dateIntervals?: Array<{ __typename?: 'DateInterval', startsAt: any, endsAt: any } | null | undefined> | null | undefined } | null | undefined> | null | undefined } | null | undefined } | null | undefined };

export type UserAddedToChannelSubscriptionVariables = Exact<{
  userId: Scalars['String'];
}>;


export type UserAddedToChannelSubscription = { __typename?: 'Subscription', communicationChannelAddedForUser?: { __typename?: 'CommunicationChannel', id: string, name: string, description: string, createdAt?: any | null | undefined, modifiedAt?: any | null | undefined } | null | undefined };

export type MessageAddedToChannelSubscriptionVariables = Exact<{
  channelId: Scalars['String'];
}>;


export type MessageAddedToChannelSubscription = { __typename?: 'Subscription', communicationChannelMessageAddedToChannel?: { __typename?: 'CommunicationChannelMessage', id: string, createdAt?: any | null | undefined, modifiedAt?: any | null | undefined, userName: string, content: string, channelId: string, userId: string, type?: CommunicationChannelMessageType | null | undefined } | null | undefined };


export const AddUserToCalendarEventDocument = gql`
    mutation AddUserToCalendarEvent($userId: String!, $eventId: String!) {
  communicationChannelCalendarEvents {
    addUser(userId: $userId, eventId: $eventId)
  }
}
    `;
export type AddUserToCalendarEventMutationFn = Apollo.MutationFunction<AddUserToCalendarEventMutation, AddUserToCalendarEventMutationVariables>;

/**
 * __useAddUserToCalendarEventMutation__
 *
 * To run a mutation, you first call `useAddUserToCalendarEventMutation` within a React component and pass it any options that fit your needs.
 * When your component renders, `useAddUserToCalendarEventMutation` returns a tuple that includes:
 * - A mutate function that you can call at any time to execute the mutation
 * - An object with fields that represent the current status of the mutation's execution
 *
 * @param baseOptions options that will be passed into the mutation, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options-2;
 *
 * @example
 * const [addUserToCalendarEventMutation, { data, loading, error }] = useAddUserToCalendarEventMutation({
 *   variables: {
 *      userId: // value for 'userId'
 *      eventId: // value for 'eventId'
 *   },
 * });
 */
export function useAddUserToCalendarEventMutation(baseOptions?: Apollo.MutationHookOptions<AddUserToCalendarEventMutation, AddUserToCalendarEventMutationVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useMutation<AddUserToCalendarEventMutation, AddUserToCalendarEventMutationVariables>(AddUserToCalendarEventDocument, options);
      }
export type AddUserToCalendarEventMutationHookResult = ReturnType<typeof useAddUserToCalendarEventMutation>;
export type AddUserToCalendarEventMutationResult = Apollo.MutationResult<AddUserToCalendarEventMutation>;
export type AddUserToCalendarEventMutationOptions = Apollo.BaseMutationOptions<AddUserToCalendarEventMutation, AddUserToCalendarEventMutationVariables>;
export const CreateChannelCalendarEventDocument = gql`
    mutation CreateChannelCalendarEvent($name: String!, $desc: String!, $startsAt: DateTime!, $endsAt: DateTime, $channelId: String, $groupId: String, $type: String!, $maximalParticipantsCount: Int) {
  communicationChannelCalendarEvents {
    create(
      name: $name
      desc: $desc
      startsAt: $startsAt
      endsAt: $endsAt
      channelId: $channelId
      groupId: $groupId
      type: $type
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
 *      type: // value for 'type'
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
export const DeleteCalendarEventDocument = gql`
    mutation DeleteCalendarEvent($id: ID!) {
  communicationChannelCalendarEvents {
    delete(id: $id)
  }
}
    `;
export type DeleteCalendarEventMutationFn = Apollo.MutationFunction<DeleteCalendarEventMutation, DeleteCalendarEventMutationVariables>;

/**
 * __useDeleteCalendarEventMutation__
 *
 * To run a mutation, you first call `useDeleteCalendarEventMutation` within a React component and pass it any options that fit your needs.
 * When your component renders, `useDeleteCalendarEventMutation` returns a tuple that includes:
 * - A mutate function that you can call at any time to execute the mutation
 * - An object with fields that represent the current status of the mutation's execution
 *
 * @param baseOptions options that will be passed into the mutation, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options-2;
 *
 * @example
 * const [deleteCalendarEventMutation, { data, loading, error }] = useDeleteCalendarEventMutation({
 *   variables: {
 *      id: // value for 'id'
 *   },
 * });
 */
export function useDeleteCalendarEventMutation(baseOptions?: Apollo.MutationHookOptions<DeleteCalendarEventMutation, DeleteCalendarEventMutationVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useMutation<DeleteCalendarEventMutation, DeleteCalendarEventMutationVariables>(DeleteCalendarEventDocument, options);
      }
export type DeleteCalendarEventMutationHookResult = ReturnType<typeof useDeleteCalendarEventMutation>;
export type DeleteCalendarEventMutationResult = Apollo.MutationResult<DeleteCalendarEventMutation>;
export type DeleteCalendarEventMutationOptions = Apollo.BaseMutationOptions<DeleteCalendarEventMutation, DeleteCalendarEventMutationVariables>;
export const RemoveUserFromCalendarEventDocument = gql`
    mutation RemoveUserFromCalendarEvent($userId: String!, $eventId: String!) {
  communicationChannelCalendarEvents {
    removeUser(userId: $userId, eventId: $eventId)
  }
}
    `;
export type RemoveUserFromCalendarEventMutationFn = Apollo.MutationFunction<RemoveUserFromCalendarEventMutation, RemoveUserFromCalendarEventMutationVariables>;

/**
 * __useRemoveUserFromCalendarEventMutation__
 *
 * To run a mutation, you first call `useRemoveUserFromCalendarEventMutation` within a React component and pass it any options that fit your needs.
 * When your component renders, `useRemoveUserFromCalendarEventMutation` returns a tuple that includes:
 * - A mutate function that you can call at any time to execute the mutation
 * - An object with fields that represent the current status of the mutation's execution
 *
 * @param baseOptions options that will be passed into the mutation, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options-2;
 *
 * @example
 * const [removeUserFromCalendarEventMutation, { data, loading, error }] = useRemoveUserFromCalendarEventMutation({
 *   variables: {
 *      userId: // value for 'userId'
 *      eventId: // value for 'eventId'
 *   },
 * });
 */
export function useRemoveUserFromCalendarEventMutation(baseOptions?: Apollo.MutationHookOptions<RemoveUserFromCalendarEventMutation, RemoveUserFromCalendarEventMutationVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useMutation<RemoveUserFromCalendarEventMutation, RemoveUserFromCalendarEventMutationVariables>(RemoveUserFromCalendarEventDocument, options);
      }
export type RemoveUserFromCalendarEventMutationHookResult = ReturnType<typeof useRemoveUserFromCalendarEventMutation>;
export type RemoveUserFromCalendarEventMutationResult = Apollo.MutationResult<RemoveUserFromCalendarEventMutation>;
export type RemoveUserFromCalendarEventMutationOptions = Apollo.BaseMutationOptions<RemoveUserFromCalendarEventMutation, RemoveUserFromCalendarEventMutationVariables>;
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
export const CreateWishDocument = gql`
    mutation CreateWish($channelId: String!, $type: String!, $minimalParticipantsCount: Int!, $dateIntervals: [DateIntervalInput!]!) {
  wishes {
    create(
      channelId: $channelId
      type: $type
      minimalParticipantsCount: $minimalParticipantsCount
      dateIntervals: $dateIntervals
    ) {
      id
      authorId
      authorName
      dateIntervals {
        startsAt
        endsAt
      }
      createdAt
      modifiedAt
    }
  }
}
    `;
export type CreateWishMutationFn = Apollo.MutationFunction<CreateWishMutation, CreateWishMutationVariables>;

/**
 * __useCreateWishMutation__
 *
 * To run a mutation, you first call `useCreateWishMutation` within a React component and pass it any options that fit your needs.
 * When your component renders, `useCreateWishMutation` returns a tuple that includes:
 * - A mutate function that you can call at any time to execute the mutation
 * - An object with fields that represent the current status of the mutation's execution
 *
 * @param baseOptions options that will be passed into the mutation, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options-2;
 *
 * @example
 * const [createWishMutation, { data, loading, error }] = useCreateWishMutation({
 *   variables: {
 *      channelId: // value for 'channelId'
 *      type: // value for 'type'
 *      minimalParticipantsCount: // value for 'minimalParticipantsCount'
 *      dateIntervals: // value for 'dateIntervals'
 *   },
 * });
 */
export function useCreateWishMutation(baseOptions?: Apollo.MutationHookOptions<CreateWishMutation, CreateWishMutationVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useMutation<CreateWishMutation, CreateWishMutationVariables>(CreateWishDocument, options);
      }
export type CreateWishMutationHookResult = ReturnType<typeof useCreateWishMutation>;
export type CreateWishMutationResult = Apollo.MutationResult<CreateWishMutation>;
export type CreateWishMutationOptions = Apollo.BaseMutationOptions<CreateWishMutation, CreateWishMutationVariables>;
export const DeleteWishDocument = gql`
    mutation DeleteWish($id: ID!) {
  wishes {
    delete(id: $id)
  }
}
    `;
export type DeleteWishMutationFn = Apollo.MutationFunction<DeleteWishMutation, DeleteWishMutationVariables>;

/**
 * __useDeleteWishMutation__
 *
 * To run a mutation, you first call `useDeleteWishMutation` within a React component and pass it any options that fit your needs.
 * When your component renders, `useDeleteWishMutation` returns a tuple that includes:
 * - A mutate function that you can call at any time to execute the mutation
 * - An object with fields that represent the current status of the mutation's execution
 *
 * @param baseOptions options that will be passed into the mutation, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options-2;
 *
 * @example
 * const [deleteWishMutation, { data, loading, error }] = useDeleteWishMutation({
 *   variables: {
 *      id: // value for 'id'
 *   },
 * });
 */
export function useDeleteWishMutation(baseOptions?: Apollo.MutationHookOptions<DeleteWishMutation, DeleteWishMutationVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useMutation<DeleteWishMutation, DeleteWishMutationVariables>(DeleteWishDocument, options);
      }
export type DeleteWishMutationHookResult = ReturnType<typeof useDeleteWishMutation>;
export type DeleteWishMutationResult = Apollo.MutationResult<DeleteWishMutation>;
export type DeleteWishMutationOptions = Apollo.BaseMutationOptions<DeleteWishMutation, DeleteWishMutationVariables>;
export const GetCalendarEventDocument = gql`
    query GetCalendarEvent($id: ID!) {
  calendarEvents {
    get(id: $id) {
      id
      startsAt
      endsAt
      name
      description
      maximalParticipantsCount
      participantsCount
      authorId
      communicationChannelId
      createdAt
      modifiedAt
      createdBy
      deletedBy
      modifiedBy
      type
    }
  }
}
    `;

/**
 * __useGetCalendarEventQuery__
 *
 * To run a query within a React component, call `useGetCalendarEventQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetCalendarEventQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetCalendarEventQuery({
 *   variables: {
 *      id: // value for 'id'
 *   },
 * });
 */
export function useGetCalendarEventQuery(baseOptions: Apollo.QueryHookOptions<GetCalendarEventQuery, GetCalendarEventQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetCalendarEventQuery, GetCalendarEventQueryVariables>(GetCalendarEventDocument, options);
      }
export function useGetCalendarEventLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetCalendarEventQuery, GetCalendarEventQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetCalendarEventQuery, GetCalendarEventQueryVariables>(GetCalendarEventDocument, options);
        }
export type GetCalendarEventQueryHookResult = ReturnType<typeof useGetCalendarEventQuery>;
export type GetCalendarEventLazyQueryHookResult = ReturnType<typeof useGetCalendarEventLazyQuery>;
export type GetCalendarEventQueryResult = Apollo.QueryResult<GetCalendarEventQuery, GetCalendarEventQueryVariables>;
export const GetCalendarEventsDocument = gql`
    query GetCalendarEvents($pageNumber: Int!, $pageSize: Int!) {
  calendarEvents {
    getVisible(pageNumber: $pageNumber, pageSize: $pageSize) {
      data {
        id
        startsAt
        endsAt
        name
        description
        maximalParticipantsCount
        participantsCount
        authorId
        communicationChannelId
        createdAt
        modifiedAt
        type
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
 * __useGetCalendarEventsQuery__
 *
 * To run a query within a React component, call `useGetCalendarEventsQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetCalendarEventsQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetCalendarEventsQuery({
 *   variables: {
 *      pageNumber: // value for 'pageNumber'
 *      pageSize: // value for 'pageSize'
 *   },
 * });
 */
export function useGetCalendarEventsQuery(baseOptions: Apollo.QueryHookOptions<GetCalendarEventsQuery, GetCalendarEventsQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetCalendarEventsQuery, GetCalendarEventsQueryVariables>(GetCalendarEventsDocument, options);
      }
export function useGetCalendarEventsLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetCalendarEventsQuery, GetCalendarEventsQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetCalendarEventsQuery, GetCalendarEventsQueryVariables>(GetCalendarEventsDocument, options);
        }
export type GetCalendarEventsQueryHookResult = ReturnType<typeof useGetCalendarEventsQuery>;
export type GetCalendarEventsLazyQueryHookResult = ReturnType<typeof useGetCalendarEventsLazyQuery>;
export type GetCalendarEventsQueryResult = Apollo.QueryResult<GetCalendarEventsQuery, GetCalendarEventsQueryVariables>;
export const GetJoinedCalendarEventsDocument = gql`
    query GetJoinedCalendarEvents($pageNumber: Int!, $pageSize: Int!) {
  calendarEvents {
    getJoined(pageNumber: $pageNumber, pageSize: $pageSize) {
      data {
        id
        startsAt
        endsAt
        name
        description
        maximalParticipantsCount
        participantsCount
        authorId
        communicationChannelId
        createdAt
        modifiedAt
        type
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
 * __useGetJoinedCalendarEventsQuery__
 *
 * To run a query within a React component, call `useGetJoinedCalendarEventsQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetJoinedCalendarEventsQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetJoinedCalendarEventsQuery({
 *   variables: {
 *      pageNumber: // value for 'pageNumber'
 *      pageSize: // value for 'pageSize'
 *   },
 * });
 */
export function useGetJoinedCalendarEventsQuery(baseOptions: Apollo.QueryHookOptions<GetJoinedCalendarEventsQuery, GetJoinedCalendarEventsQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetJoinedCalendarEventsQuery, GetJoinedCalendarEventsQueryVariables>(GetJoinedCalendarEventsDocument, options);
      }
export function useGetJoinedCalendarEventsLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetJoinedCalendarEventsQuery, GetJoinedCalendarEventsQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetJoinedCalendarEventsQuery, GetJoinedCalendarEventsQueryVariables>(GetJoinedCalendarEventsDocument, options);
        }
export type GetJoinedCalendarEventsQueryHookResult = ReturnType<typeof useGetJoinedCalendarEventsQuery>;
export type GetJoinedCalendarEventsLazyQueryHookResult = ReturnType<typeof useGetJoinedCalendarEventsLazyQuery>;
export type GetJoinedCalendarEventsQueryResult = Apollo.QueryResult<GetJoinedCalendarEventsQuery, GetJoinedCalendarEventsQueryVariables>;
export const GetChannelDocument = gql`
    query GetChannel($id: ID!) {
  communicationChannels {
    get(id: $id) {
      id
      name
      description
      createdAt
      modifiedAt
      createdBy
      deletedBy
      modifiedBy
    }
  }
}
    `;

/**
 * __useGetChannelQuery__
 *
 * To run a query within a React component, call `useGetChannelQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetChannelQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetChannelQuery({
 *   variables: {
 *      id: // value for 'id'
 *   },
 * });
 */
export function useGetChannelQuery(baseOptions: Apollo.QueryHookOptions<GetChannelQuery, GetChannelQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetChannelQuery, GetChannelQueryVariables>(GetChannelDocument, options);
      }
export function useGetChannelLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetChannelQuery, GetChannelQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetChannelQuery, GetChannelQueryVariables>(GetChannelDocument, options);
        }
export type GetChannelQueryHookResult = ReturnType<typeof useGetChannelQuery>;
export type GetChannelLazyQueryHookResult = ReturnType<typeof useGetChannelLazyQuery>;
export type GetChannelQueryResult = Apollo.QueryResult<GetChannelQuery, GetChannelQueryVariables>;
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
    query GetUsers($searchTerm: String!, $excludedUserIds: [String], $channelId: String, $groupId: String) {
  users {
    get(
      searchTerm: $searchTerm
      excludedUserIds: $excludedUserIds
      channelId: $channelId
      groupId: $groupId
    ) {
      data {
        id
        userName
        createdAt
        modifiedAt
        createdBy
        deletedBy
        modifiedBy
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
 *      excludedUserIds: // value for 'excludedUserIds'
 *      channelId: // value for 'channelId'
 *      groupId: // value for 'groupId'
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
export const GetUsersForCalendarEventDocument = gql`
    query GetUsersForCalendarEvent($calendarEventId: ID!) {
  users {
    getForCalendarEvent(calendarEventId: $calendarEventId) {
      data {
        id
        userName
        createdAt
        modifiedAt
        createdBy
        deletedBy
        modifiedBy
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
 * __useGetUsersForCalendarEventQuery__
 *
 * To run a query within a React component, call `useGetUsersForCalendarEventQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetUsersForCalendarEventQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetUsersForCalendarEventQuery({
 *   variables: {
 *      calendarEventId: // value for 'calendarEventId'
 *   },
 * });
 */
export function useGetUsersForCalendarEventQuery(baseOptions: Apollo.QueryHookOptions<GetUsersForCalendarEventQuery, GetUsersForCalendarEventQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetUsersForCalendarEventQuery, GetUsersForCalendarEventQueryVariables>(GetUsersForCalendarEventDocument, options);
      }
export function useGetUsersForCalendarEventLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetUsersForCalendarEventQuery, GetUsersForCalendarEventQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetUsersForCalendarEventQuery, GetUsersForCalendarEventQueryVariables>(GetUsersForCalendarEventDocument, options);
        }
export type GetUsersForCalendarEventQueryHookResult = ReturnType<typeof useGetUsersForCalendarEventQuery>;
export type GetUsersForCalendarEventLazyQueryHookResult = ReturnType<typeof useGetUsersForCalendarEventLazyQuery>;
export type GetUsersForCalendarEventQueryResult = Apollo.QueryResult<GetUsersForCalendarEventQuery, GetUsersForCalendarEventQueryVariables>;
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
export const GetActiveWishesDocument = gql`
    query GetActiveWishes($pageNumber: Int!, $pageSize: Int!) {
  wishes {
    getActive(pageNumber: $pageNumber, pageSize: $pageSize) {
      data {
        id
        authorId
        authorName
        dateIntervals {
          startsAt
          endsAt
        }
        maximalParticipantsCount
        minimalParticipantsCount
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
 * __useGetActiveWishesQuery__
 *
 * To run a query within a React component, call `useGetActiveWishesQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetActiveWishesQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetActiveWishesQuery({
 *   variables: {
 *      pageNumber: // value for 'pageNumber'
 *      pageSize: // value for 'pageSize'
 *   },
 * });
 */
export function useGetActiveWishesQuery(baseOptions: Apollo.QueryHookOptions<GetActiveWishesQuery, GetActiveWishesQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetActiveWishesQuery, GetActiveWishesQueryVariables>(GetActiveWishesDocument, options);
      }
export function useGetActiveWishesLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetActiveWishesQuery, GetActiveWishesQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetActiveWishesQuery, GetActiveWishesQueryVariables>(GetActiveWishesDocument, options);
        }
export type GetActiveWishesQueryHookResult = ReturnType<typeof useGetActiveWishesQuery>;
export type GetActiveWishesLazyQueryHookResult = ReturnType<typeof useGetActiveWishesLazyQuery>;
export type GetActiveWishesQueryResult = Apollo.QueryResult<GetActiveWishesQuery, GetActiveWishesQueryVariables>;
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