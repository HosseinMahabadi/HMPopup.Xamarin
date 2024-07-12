using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HMPopup
{
    public interface IPopup
    {
        /// <summary>
        /// Displays a message with a confirmation button
        /// </summary>
        /// <param name="Title">Title to display the message at the top</param>
        /// <param name="Message">Message to display</param>
        void ShowMessage(string Title, string Message);

        /// <summary>
        /// Displays a asynchronous message with a confirmation button
        /// </summary>
        /// <param name="Title">Title to display the message at the top</param>
        /// <param name="Message">Message to display</param>
        Task ShowMessageAsync(string Title, string Message);

        /// <summary>
        /// Displays a asynchronous message with a confirmation button
        /// </summary>
        /// <param name="Title">Title to display the message at the top</param>
        /// <param name="Message">Message to display</param>
        /// <param name="OkTitle">Confirm button title</param>
        /// <returns></returns>
        Task ShowMessageAsync(string Title, string Message, string OkTitle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Question"></param>
        /// <returns></returns>
        Task<bool> ShowQuestionAsync(string Title, string Question);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Question"></param>
        /// <param name="YesTitle"></param>
        /// <param name="NoTitle"></param>
        /// <returns></returns>
        Task<bool> ShowQuestionAsync(string Title, string Question, string YesTitle, string NoTitle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Message"></param>
        /// <param name="Items"></param>
        /// <returns></returns>
        Task<T> ShowSelectionAsync<T>(string Title, string Message, IList<T> Items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Message"></param>
        /// <param name="Items"></param>
        /// <param name="SelectedItem"></param>
        /// <returns></returns>
        Task<T> ShowSelectionAsync<T>(string Title, string Message, IList<T> Items, T SelectedItem);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Message"></param>
        /// <param name="Items"></param>
        /// <param name="SelectTitle"></param>
        /// <param name="CancelTitle"></param>
        /// <returns></returns>
        Task<T> ShowSelectionAsync<T>(string Title, string Message, IList<T> Items, string SelectTitle, string CancelTitle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Message"></param>
        /// <param name="Items"></param>
        /// <param name="SelectedItem"></param>
        /// <param name="SelectTitle"></param>
        /// <param name="CancelTitle"></param>
        /// <returns></returns>
        Task<T> ShowSelectionAsync<T>(string Title, string Message, IList<T> Items, T SelectedItem, string SelectTitle, string CancelTitle);
    }
}
